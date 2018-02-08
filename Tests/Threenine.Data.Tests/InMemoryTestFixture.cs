using System;
using Microsoft.EntityFrameworkCore;
using TestDatabase;

namespace Threenine.Data.Tests
{
    public class InMemoryTestFixture : IDisposable
    {
        public TestDbContext Context => InMemoryContext();

        public void Dispose()
        {
            Context?.Dispose();
        }

        private TestDbContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new TestDbContext(options);

            return context;
        }
    }
}