using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using TestDatabase;

namespace Threenine.Data.Tests.TestFixtures
{
    public class SqlLiteReadOnlyTestFixture : IDisposable
    {
        public ClonedTestDbContext Context => SqlLiteInMemoryContext();

        public void Dispose()
        {
            Context?.Database.CloseConnection();
            Context?.Dispose();
        }

        private ClonedTestDbContext SqlLiteInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ClonedTestDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new ClonedTestDbContext(options);
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