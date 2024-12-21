using Microsoft.EntityFrameworkCore;

namespace LoanApiCommSchool.Models
{
    

    
    public class LoanDBContext : DbContext
    {
        public LoanDBContext(DbContextOptions<LoanDBContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<Accountant> Accountant { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .Property(l => l.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<User>()
                .Property(u => u.MonthlyIncome)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);


        }

    }
}
