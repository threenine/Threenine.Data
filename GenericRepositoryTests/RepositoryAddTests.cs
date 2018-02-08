using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data;
using Xunit;

namespace Threenine.Data.Tests
{
    public class RepositoryAddTest : IClassFixture<InMemoryTestFixture>
    {
        private readonly InMemoryTestFixture _fixture;
        public RepositoryAddTest(InMemoryTestFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void ShouldAddNewProduct()
        {
            // Arrange 
            var uow = new UnitOfWork<TestDbContext>(_fixture.InMemoryContext());
            var repo = uow.GetRepository<TestProduct>();
            var newProduct  = new TestProduct(){Name = "Test Product"};

            // Act
            repo.Add(newProduct);
            uow.SaveChanges();

            //Assert
            Assert.Equal(1,newProduct.Id);

        }


      
    }
}
