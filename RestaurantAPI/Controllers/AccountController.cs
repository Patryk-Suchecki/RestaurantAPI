using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.entity;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }
        [HttpGet("statistics")]
        public ActionResult Statistics([FromQuery]string email)
        {
            
            var result = _accountService.UserStatistics(email);
            return Ok(result);
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
        [HttpGet("verify")]
        public ActionResult Verify([FromQuery] string email, int? code)
        {
            _accountService.VerifyEmail(email, code);
            return Ok();
        }
    }
}
