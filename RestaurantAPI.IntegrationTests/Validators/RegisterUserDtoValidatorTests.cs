﻿using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.entity;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Validators;

namespace RestaurantAPI.IntegrationTests.Validators
{

    public class RegisterUserDtoValidatorTests
    {
        private RestaurantDbContext _dbContext;
        public RegisterUserDtoValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase("TestDb");

            _dbContext = new RestaurantDbContext(builder.Options);
            Seed();
        }

        public void Seed()
        {
            var testUsers = new List<User>()
            {
                new User()
                {
                    Email= "test2@test.com",
                },
                new User()
                {
                    Email= "test3@test.com",
                },
            };

            _dbContext.Users.AddRange(testUsers);
            _dbContext.SaveChanges();
        }

        [Fact]
        public void Validate_ForValidModel_ReturnsSuccess()
        {
            //arrange
            var model = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "test1234",
                ConfirmPassword = "test1234"
            };

            var validator = new RegisterUserDtoValidator(_dbContext);

            // act
            var result = validator.TestValidate(model);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
