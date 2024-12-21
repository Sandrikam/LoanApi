namespace LoanApiCommSchool.Models
{
    public class Loan
    {
        public int ID { get; set; }
        public string LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
        public string Status { get; set; }
        // Foreign Key
        public int UserID { get; set; }
        // Navigation Property
        public User User { get; set; }
    }
}
