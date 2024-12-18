using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApiCommSchool.Controllers
{
    public class AcountantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
