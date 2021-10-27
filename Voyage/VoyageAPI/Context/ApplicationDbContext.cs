using Microsoft.EntityFrameworkCore;
using VoyageAPI.Models;

namespace VoyageAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().HasOne(r => r.Employee);
            modelBuilder.Entity<Report>().HasOne(p => p.Product);
            modelBuilder.Entity<Report>().HasMany(i => i.Images);
            modelBuilder.Entity<Job>().HasOne(r => r.Employee);
            modelBuilder.Entity<Job>().HasOne(r => r.Product);
            modelBuilder.Entity<Product>().HasMany(p => p.Jobs);
            modelBuilder.Entity<Employee>().HasMany(e => e.Jobs);
            modelBuilder.Entity<Employee>().HasMany(r => r.Reports);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
