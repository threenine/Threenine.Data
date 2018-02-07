using System;
using Microsoft.EntityFrameworkCore;

namespace TestDatabase
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
           
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestProduct> TestProducts { get; set; }
    }
}
