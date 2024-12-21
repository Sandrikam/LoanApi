using LoanApiCommSchool.Models;
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

            _context.User.Add(user); // Add user to the database
            _context.SaveChanges(); // Save changes
            return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
        }
    }
}
