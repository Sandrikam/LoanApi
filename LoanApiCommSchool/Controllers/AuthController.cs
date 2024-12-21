using LoanApiCommSchool.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = JWTTokenGenerator.GenerateToken(login.Username, "Admin", _configuration);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
    
}
