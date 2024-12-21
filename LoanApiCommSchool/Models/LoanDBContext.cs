using Microsoft.EntityFrameworkCore;

namespace LoanApiCommSchool.Models
{
    public class LoanDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
