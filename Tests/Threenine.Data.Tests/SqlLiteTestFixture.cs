using System;
using Microsoft.EntityFrameworkCore;
using TestDatabase;

namespace Threenine.Data.Tests
{
    public class SqlLiteTestFixture : IDisposable
    {
        public TestDbContext Context => SqlLiteInMemoryContext();

        public void Dispose()
        {
            Context?.Dispose();
        }

        private TestDbContext SqlLiteInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;


            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
             return context;
        }
    }
}