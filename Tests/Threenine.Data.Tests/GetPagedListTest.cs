using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data.Paging;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("PagedList")]
    public class GetPagedListTest : IDisposable
    {
        public GetPagedListTest(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _testFixture;
       
        [Fact]
        public void GetProductPagedListUsingPredicateTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(predicate: x => x.CategoryId == 1).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }

        [Fact]
        public void ShouldGet5ProductsOutOfStockMultiPredicateTest()
        {
            // Arrange
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(predicate: x =>  x.Stock == 0 && x.InStock.Value == false).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }
        [Fact]
        public void ShouldGetAllProductsFromSqlQuerySelect()
        
        {
            // Arrange
            var strSQL = "Select * from TestProduct";
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.Query(strSQL).AsEnumerable();
            //Assert
            Assert.Equal(20, productList.Count());
        }
        
        [Fact]
        public void ShouldGetSqlQuerySelect()
        {
            //Arrange
            var strSQL = "Select p.* from TestProduct p inner join TestCategory c on p.categoryid = c.id where c.id = 1";
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.Query(strSQL).AsEnumerable();
            //Assert
            Assert.Equal(5, productList.Count());
        }


        [Fact]
        public void ShouldBeReadOnlyInterface()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            //Act
            var repo = uow.GetReadOnlyRepository<TestProduct>();
            //Assert
            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
        }

        [Fact]
        public void ShouldReadFromProducts()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();
            //Act 
            var products = repo.GetList().Items;
            //Assert
            Assert.Equal(20, products.Count);
        }
    }
}