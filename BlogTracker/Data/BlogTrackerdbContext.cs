using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogTracker.Models;

namespace BlogTracker.Data
{
    public class BlogTrackerdbContext : DbContext
    {
        public BlogTrackerdbContext(DbContextOptions<BlogTrackerdbContext> options)
            : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed admin details
            modelBuilder.Entity<AdminInfo>().HasData(new AdminInfo
            {
                EmailId = "admin@gmail.com",
                Password = "admin123" // Make sure to hash this password in a real application!
            });
        }

        public DbSet<BlogTracker.Models.AdminInfo> AdminInfo { get; set; } = default!;

        public DbSet<BlogTracker.Models.EmpInfo>? EmpInfo { get; set; }

        public DbSet<BlogTracker.Models.BlogInfo>? BlogInfo { get; set; }
       

    }
  
}

