using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
    public class SqlLiteTestFixture : IDisposable
    {


        public TestDbContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite("DataSource=:memory:")

                .Options;


            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            var testCat = new TestCategory() { Id = 1, Name =GlobalTestStrings.TestProductCategoryName };

            context.TestCategories.Add(testCat);
            context.SaveChanges();
            return context;
        }
        public void Dispose()
        {
        }
    }
}
