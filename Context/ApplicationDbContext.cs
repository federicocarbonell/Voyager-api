using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            modelBuilder.Entity<Job>().HasOne(r => r.Employee);
            modelBuilder.Entity<Job>().HasOne(r => r.Product);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
