using LoanApiCommSchool.Models;
using LoanApiCommSchool.Methods;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult GetAllLoans()
        {
            var userId = TokenReader.GetUserIdFromToken(User);
            var role = TokenReader.GetRoleFromToken(User);

            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID claim is missing in the token." });
            }

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
            var userId = TokenReader.GetUserIdFromToken(User);
            var role = TokenReader.GetRoleFromToken(User);

            if (userId == null)
                return Unauthorized(new { Message = "User ID claim is missing in the token." });

            // Accountant can view any loan, users can only view their own
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
            var userId = TokenReader.GetUserIdFromToken(User);
            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID claim is missing in the token." });
            }

            var user = _context.User.FirstOrDefault(u => u.ID == userId);
            if (user == null || user.IsBlocked)
            {
                return StatusCode(403, new { Message = "Blocked users cannot request loans." });
            }

            loan.UserID = userId.Value;
            loan.Status = "Processing";

            _context.Loan.Add(loan);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLoanById), new { id = loan.ID }, loan);
        }

        // PUT: api/Loan/{id}
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateLoan(int id, [FromBody] Loan updatedLoan)
        {

            var userId = TokenReader.GetUserIdFromToken(User);
            var role = TokenReader.GetRoleFromToken(User);

            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID claim is missing in the token." });
            }

            var loan = _context.Loan.FirstOrDefault(l => l.ID == id);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }

            if (role != "Accountant" && (loan.UserID != userId || loan.Status != "Processing"))
            {
                return StatusCode(403, new { Message = "You can only update your own loans with status 'Processing'." });
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
        [Authorize]
        public IActionResult DeleteLoan(int id)
        {

            var userId = TokenReader.GetUserIdFromToken(User);
            var role = TokenReader.GetRoleFromToken(User);

            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID claim is missing in the token." });
            }
                

            var loan = _context.Loan.FirstOrDefault(l => l.ID == id);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found" });
            }
                

            if (role != "Accountant" && (loan.UserID != userId || loan.Status != "Processing"))
            {
                return StatusCode(403, new { Message = "You can only delete your own loans with status 'Processing'." });
            }
                
            _context.Loan.Remove(loan);

            _context.SaveChanges();
  
            return Ok(new { Message = "Loan deleted successfully" });
        }
    }
}
