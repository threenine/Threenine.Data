using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
  public  class RepositoryAddTestsSqlLite : IClassFixture<SqlLiteTestFixture>
  {
      private readonly SqlLiteTestFixture _fixture;
      public RepositoryAddTestsSqlLite(SqlLiteTestFixture fixture)
      {
          _fixture = fixture;
      }
     
        [Fact]
        public void ShouldAddNewProduct()
        {
           
            //Arrange
            var uow = new UnitOfWork<TestDbContext>(_fixture.SqlLiteInMemoryContext());
            var repo = uow.GetRepository<TestProduct>();
            var newProduct = new TestProduct() { Name = GlobalTestStrings.TestProductName , Category = new TestCategory() { Id = 1, Name = GlobalTestStrings.TestProductCategoryName } };

            //Act 
            repo.Add(newProduct);
            uow.SaveChanges();

            //Assert
            Assert.Equal(1, newProduct.Id);

        }


    }
}
