using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestDatabase;

namespace Threenine.Data.Tests
{
   public class InMemoryTestFixture :  IDisposable
   {
       public TestDbContext Context => InMemoryContext();
        private TestDbContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new TestDbContext(options);
            
            return context;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
