using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
  public  class RepositoryAddTestsSQLLite
    {
        [Fact]
        public void ShouldAddNewProduct()
        {
            var uow = new UnitOfWork(GetInMemoryContext());
            var repo = new Repository<TestProduct>(uow);

            var newProduct = new TestProduct() { Name = "Test Product", Category = new TestCategory() { Id = 1, Name = "UNi TEtS" } };

            repo.Add(newProduct);
            uow.Commit();

            Assert.Equal(1, newProduct.Id);

        }


        private TestDbContext GetInMemoryContext()
        {
          
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite("DataSource=:memory:")
                .EnableSensitiveDataLogging()
                .Options;

       
            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            var testCat = new TestCategory(){ Id = 1, Name = "UNi TEtS"};

            context.TestCategories.Add(testCat);
            context.SaveChanges();
            return context;
        }
    }
}
