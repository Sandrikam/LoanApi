using Microsoft.EntityFrameworkCore;
using System.Data;
using LoanApiCommSchool.Models;
using System.Collections.Generic;

namespace LoanApiCommSchool.Data
{
    public class UserContext : DbContext
    {
        // declare DBSets
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=COMMProjectDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
