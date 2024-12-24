using LoanApiCommSchool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

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
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            // Accountant Loan Visibility
            var loans = role == "Accountant"
                ? _context.Loan.ToList()
                : _context.Loan.Where(l => l.UserID == userId).ToList();

            return Ok(loans);
        }

        // GET: api/Loan/{id}
        [HttpGet("{id}")]
        public IActionResult GetLoanById(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var loan = role == "Accountant"
                ? _context.Loan.FirstOrDefault(l => l.ID == id)
                : _context.Loan.FirstOrDefault(l => l.ID == id && l.UserID == userId);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }

            return Ok(loan);
        }

        // POST: api/Loan
        [HttpPost]
        [Authorize]
        public IActionResult AddLoan([FromBody] Loan loan)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (loan == null)
            {
                return BadRequest(new { Message = "Invalid loan data" });
            }

            var user = _context.User.FirstOrDefault(u => u.ID == userId);
            if (user == null || user.IsBlocked)
            {
                return Forbid(new { Message = "Blocked users cannot request loans." });
            }

            loan.UserID = userId;
            loan.Status = "Processing"; //Def Status

            _context.Loan.Add(loan);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.ID }, loan);
        }

        private IActionResult Forbid(object value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Loan/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateLoan(int id, [FromBody] Loan updatedLoan)
        {

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Accountant")
            {
                return Forbid(new { Message = "Only accountants can update loans." });
            }

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

            var userId = int.Parse(User.FindFirst("UserId").Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var loan = role == "Accountant"
                ? _context.Loan.FirstOrDefault(l => l.ID == id)
                : _context.Loan.FirstOrDefault(l => l.ID == id && l.UserID == userId);

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
