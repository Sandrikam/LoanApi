﻿using LoanApiCommSchool.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LoanApiCommSchool.Models;
using System.Linq;
using System;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly LoanDBContext _context;

        public AuthController(IConfiguration configuration, LoanDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest(new { Message = "Invalid username or password" });
            }

            if (login.Username == "admin" && login.Password == "admin")
            {
                // Set role to "Accountant" without querying the database
                var tokeen = JWTTokenGenerator.GenerateToken(login.Username, "Accountant", 0, _configuration);
                return Ok(new { Token = tokeen });
            }

            //hash Password to md5
            var hashedPassword = md5Encryptor.HashPasswordMD5(login.Password);

            var user = _context.User.FirstOrDefault(u => u.Username == login.Username && u.Password == hashedPassword);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            // Determine the role by checking the Accountant table
            var role = _context.Accountant.Any(a => a.UserId == user.ID) ? "Accountant" : "User";

            // Generate JWT token
            var token = JWTTokenGenerator.GenerateToken(user.Username, role, user.ID, _configuration);

            Console.WriteLine($"Bearer {token}");

            return Ok(new { Token = token });
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
    
}
