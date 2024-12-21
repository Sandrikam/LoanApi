using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcountantController : Controller
    {
        [HttpGet]
        public IActionResult GetAccountant()
        {
            return Ok(new { ID = 1, 
                            FirstName = "Habib", 
                            LastName = "Nurmagomedov"
                            });
        }
    }
}
