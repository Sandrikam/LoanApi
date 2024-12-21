using LoanApiCommSchool.Methods;
using LoanApiCommSchool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly LoanDBContext _context;

        public UserController(LoanDBContext context)
        {
            _context = context;
        }

        //Get Users Endpoint
        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _context.User.ToList(); 
            return Ok(user);
        }

        //Get User By ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.User.FirstOrDefault(u => u.ID == id); // Find user by ID
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }

        //Create User
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            //encrypt Password
            user.PasswordHash = md5Encryptor.HashPasswordMD5(user.PasswordHash);

            try
            {
                _context.User.Add(user); // Add user to the database
                _context.SaveChanges(); // Save changes
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                throw;
            }

            
            return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.ID)
            {
                return BadRequest(new { Message = "Invalid user data or ID mismatch" });
            }

            var user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Update user fields
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Username = updatedUser.Username;
            user.Age = updatedUser.Age;
            user.Email = updatedUser.Email;
            user.MonthlyIncome = updatedUser.MonthlyIncome;
            user.IsBlocked = updatedUser.IsBlocked;
            user.PasswordHash = updatedUser.PasswordHash;

            _context.SaveChanges();
            return Ok(new { Message = "User updated successfully", User = user });
        }
    }
}
