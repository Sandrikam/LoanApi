using LoanApiCommSchool.Methods;
using LoanApiCommSchool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly LoanDBContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(LoanDBContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Accountant")]
        public IActionResult GetUser([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var users = _context.User.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users.", Error = ex.Message });
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var userId = TokenReader.GetUserIdFromToken(User);
                var role = TokenReader.GetRoleFromToken(User);

                if (userId == null)
                {
                    return Unauthorized(new { Message = "User ID claim is missing in the token." });
                }


                if (role == "User" && userId != id)
                {
                    return StatusCode(403, new { Message = "You do not have permission to access this user's details." });
                }

                var user = _context.User.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }
                return Ok( new 
                { 
                    user.ID,
                    user.FirstName,
                    user.LastName,
                    user.Username,
                    user.Age,
                    user.Email,
                    user.MonthlyIncome,
                    user.IsBlocked
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the user.", Error = ex.Message });
            }
        }

        // POST: api/User/Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            _logger.LogInformation("Register endpoint hit with Username: {Username}", user.Username);

            if (user == null || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "Invalid user data or missing password." });
            }

            try
            {
                // Check if the username already exists
                if (_context.User.Any(u => u.Username == user.Username))
                {
                    return Conflict(new { Message = "Username is already taken." });
                }

                // Hash the password before storing
                user.Password = md5Encryptor.HashPasswordMD5(user.Password);

                // Default IsBlocked value
                user.IsBlocked = false;

                // Add user to the database
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
                //Check TOken
                var userId = TokenReader.GetUserIdFromToken(User);
                var role = TokenReader.GetRoleFromToken(User);

                if (userId == null)
                {
                    return Unauthorized(new { Message = "User ID claim is missing in the token." });
                }

                // Check if the logged-in user is allowed to update this user
                if (role != "Accountant" && userId != id)
                {
                    return StatusCode(403, new { Message = "You do not have permission to Update Details of this user!" });
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

        [HttpPut("block/{id}")]
        [Authorize(Roles = "Accountant")]
        public IActionResult BlockUser(int id)
        {
            var user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.IsBlocked = true;
            _context.SaveChanges();

            return Ok(new { Message = "User blocked successfully." });
        }

        [HttpPut("unblock/{id}")]
        [Authorize(Roles = "Accountant")]
        public IActionResult UnblockUser(int id)
        {
            var user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.IsBlocked = false;
            _context.SaveChanges();

            return Ok(new { Message = "User unblocked successfully." });
        }

    }
}
