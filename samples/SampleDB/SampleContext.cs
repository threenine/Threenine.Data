using Microsoft.EntityFrameworkCore;
using Sample.Entity;

namespace Sample
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>();

            base.OnModelCreating(modelBuilder);
        }
    }
}