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

        // GET: api/User
        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                var users = _context.User.ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users.", Error = ex.Message });
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.User.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the user.", Error = ex.Message });
            }
        }

        // POST: api/User
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "Invalid user data or missing password." });
            }

            try
            {
                // Encrypt Password
                user.Password = md5Encryptor.HashPasswordMD5(user.Password);

                _context.User.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the user.", Error = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.ID)
            {
                return BadRequest(new { Message = "Invalid user data or ID mismatch." });
            }

            try
            {
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

                // Hash password if it is updated
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    user.Password = md5Encryptor.HashPasswordMD5(updatedUser.Password);
                }

                _context.SaveChanges();
                return Ok(new { Message = "User updated successfully", User = user });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the user.", Error = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }
    }
}
