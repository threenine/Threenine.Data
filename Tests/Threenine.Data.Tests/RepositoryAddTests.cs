using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data;
using Xunit;

namespace Threenine.Data.Tests
{
    public class RepositoryAddTests
    {
        [Fact]
        public void ShouldAddNewProduct()
        {
            var uow = new UnitOfWork(GetInMemoryContext());
            var repo = new Repository<TestProduct>(uow);
          
            var newProduct  = new TestProduct(){Name = "Test Product"};

            repo.Add(newProduct);
            uow.Commit();

            Assert.Equal(1,newProduct.Id);

        }


        private TestDbContext GetInMemoryContext()
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
