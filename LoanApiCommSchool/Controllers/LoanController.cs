using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        [HttpGet]
        public IActionResult GetLoan()
        {
            return Ok(new { LoanType = "installment", 
                            LoanID = "ZC123542", 
                            Amount = 15000,
                            LoanCCY = "USD",
                            Term = 120,
                            Status = "Active"
                            });
        }
    }
}
