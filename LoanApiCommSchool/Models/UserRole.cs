namespace LoanApiCommSchool.Models
{
    public class UserRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
