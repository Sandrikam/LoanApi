using Microsoft.EntityFrameworkCore;

namespace LoanApiCommSchool.Models
{
    

    
    public class LoanDBContext : DbContext
    {
        public LoanDBContext(DbContextOptions<LoanDBContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasNoKey();
        }

    }
}
