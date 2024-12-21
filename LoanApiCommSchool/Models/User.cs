using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace LoanApiCommSchool.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string Password { get; set; } 
    }
}
