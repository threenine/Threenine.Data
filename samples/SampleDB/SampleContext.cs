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
            modelBuilder.Entity<Person>(entity =>
                {
                    entity.HasData(
                        new Person
                        {
                            Email = "foo@foo.com", Id = 1, Title = "Mr", FirstName = "Foo", LastName = "Bar",
                            Profile = "Foo Bar Bar Foo", TagLine = "FooBar"
                        },
                        new Person
                        {
                            Email = "foo2@foo2.com", Id = 2, Title = "Mr", FirstName = "Foo2", LastName = "Bar2",
                            Profile = "Foo Bar Bar Foo2", TagLine = "FooBar2"
                        },
                        new Person
                        {
                            Email = "foo3@foo3.com", Id = 3, Title = "Mr", FirstName = "Foo3", LastName = "Bar3",
                            Profile = "Foo Bar Bar Foo3", TagLine = "FooBar3"
                        }
                        );
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}