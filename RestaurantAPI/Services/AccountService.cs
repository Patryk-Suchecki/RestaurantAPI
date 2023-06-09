﻿using System.Linq;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.entity;
using RestaurantAPI.Models;
using RestaurantAPI.Exceptions;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void VerifyEmail(string email, int? code);
        UserStatisticsDto UserStatistics(string email);
    }

    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IEmailSender emailSender, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _emailSender = emailSender;
            _mapper = mapper;
        }
        public UserStatisticsDto UserStatistics(string email)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Email == email);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var result = _mapper.Map<UserStatisticsDto>(user);
            return result;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash= hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
        public string GenerateJwt(LoginDto dto) 
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user == null) 
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed) 
            {
                throw new BadRequestException("Invalid username or password");
            }
            user.NumberOfLogins++;
            _dbContext.SaveChanges();
            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("Nationality", user.Nationality)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public void VerifyEmail(string email, int? code)
        {
            string verificationLink = "https://example.com/verify?email=" + email + "&code=123456";

            string subject = "Weryfikacja adresu email";
            string body = "Kliknij w poniższy link, aby zweryfikować swój adres email: " + verificationLink;
            _emailSender.SendEmailAsync(email, subject, body);
        }
    }
}
