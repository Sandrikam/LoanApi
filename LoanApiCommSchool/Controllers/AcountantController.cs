using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    public class AcountantController : Controller
    {
        [Route("api/[controller]")]
        public IActionResult GetAccountant()
        {
            return Ok(new { ID = 1, 
                            FirstName = "Habib", 
                            LastName = "Nurmagomedov"
                            });
        }
    }
}
