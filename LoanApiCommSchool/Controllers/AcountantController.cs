using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    public class AcountantController : Controller
    {
        public IActionResult GetAccountant()
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
