using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace LoanApiCommSchool.Models
{
    public class Accountant
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
    }
}
