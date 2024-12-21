using LoanApiCommSchool.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(new { ID = 1, 
                            FirstName = "ZC123542", 
                            LastName = 15000,
                            UserName = "USD",
                            Age = 120,
                            Salary = "Active",
                            isBlocked = 0,
                            Passwod = "shambala"
                            });
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            return Ok(new
            {
                ID = 1,
                FirstName = "ZC123542",
                LastName = 15000,
                UserName = "USD",
                Age = 120,
                Salary = "Active",
                isBlocked = 0,
                Passwod = "shambala"
            });
        }
    }
}
