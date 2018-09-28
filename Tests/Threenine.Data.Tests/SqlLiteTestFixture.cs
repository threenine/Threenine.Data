using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
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
            context.TestCategories.AddRange(TestCategories());
            context.TestProducts.AddRange(TestProducts());
            context.SaveChanges();
             return context;
        }
        
        private List<TestCategory> TestCategories()
        {
            return Builder<TestCategory>.CreateListOfSize(20).Build().ToList();
        }
        private List<TestProduct> TestProducts()
        {
            var productList = Builder<TestProduct>.CreateListOfSize(20).TheFirst(5).With(x => x.CategoryId = 1)
                .Build().ToList();
            return productList;

        }
    }
}