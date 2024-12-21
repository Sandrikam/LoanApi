using LoanApiCommSchool.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LoanApiCommSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LoanDBContext _context;

        public LoanController(LoanDBContext context)
        {
            _context = context;
        }

        // GET: api/Loan
        [HttpGet]
        public IActionResult GetAllLoans()
        {
            var loans = _context.Loan.ToList();
            return Ok(loans);
        }

        // GET: api/Loan/{id}
        [HttpGet("{id}")]
        public IActionResult GetLoanById(int id)
        {
            var loan = _context.Loan.FirstOrDefault(l => l.ID == id);
            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }
            return Ok(loan);
        }

        // POST: api/Loan
        [HttpPost]
        public IActionResult AddLoan([FromBody] Loan loan)
        {
            if (loan == null)
            {
                return BadRequest(new { Message = "Invalid loan data" });
            }

            _context.Loan.Add(loan);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.ID }, loan);
        }

        // PUT: api/Loan/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateLoan(int id, [FromBody] Loan updatedLoan)
        {
            if (updatedLoan == null || id != updatedLoan.ID)
            {
                return BadRequest(new { Message = "Invalid loan data or ID mismatch" });
            }

            var loan = _context.Loan.FirstOrDefault(l => l.ID == id);
            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }

            // Update loan fields
            loan.LoanType = updatedLoan.LoanType;
            loan.Amount = updatedLoan.Amount;
            loan.Currency = updatedLoan.Currency;
            loan.Period = updatedLoan.Period;
            loan.Status = updatedLoan.Status;

            _context.SaveChanges();
            return Ok(new { Message = "Loan updated successfully", Loan = loan });
        }

        // DELETE: api/Loan/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            var loan = _context.Loan.FirstOrDefault(l => l.ID == id);
            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }

            _context.Loan.Remove(loan);
            _context.SaveChanges();
            return Ok(new { Message = "Loan deleted successfully" });
        }
    }
}
