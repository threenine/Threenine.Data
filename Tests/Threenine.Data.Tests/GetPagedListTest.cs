using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TestDatabase;
using Xunit;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Threenine.Data.Tests.TestFixtures;

namespace Threenine.Data.Tests
{
   [Collection("PagedList")]
    public class GetPagedListTest : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _testFixture;
        public GetPagedListTest(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }
       
        
        [Fact]
        public void GetProductPagedListUsingPredicateTest()
        {
            //Arrange 
            using (var uow = new UnitOfWork<TestDbContext>(_testFixture.Context))
            {
                var repo = uow.GetRepository<TestProduct>();
                //Act
                var productList = repo.GetList(predicate: x => x.CategoryId == 1).Items;
                //Assert
                Assert.Equal(5, productList.Count);
               
            }
        }

        [Fact]
        public void GetPagedListIncludesTest()
        {
            using (var uow = new UnitOfWork<TestDbContext>(_testFixture.Context))
            {
                var cats = uow.GetRepository<TestCategory>().GetList(include: source =>
                    source.Include(x => x.Products).ThenInclude(prod => prod.Category), size: 5);

                Assert.IsAssignableFrom<Paging.Paginate<TestCategory>>(cats);

                Assert.Equal( 20, cats.Count);
                Assert.Equal(4, cats.Pages);
                Assert.Equal(5, cats.Items.Count);
                
              
            }

        }
        [Fact]
        public void ShouldBeReadOnlyInterface()
        {
            // Arrange 
            using (var uow = new UnitOfWork<TestDbContext>(_testFixture.Context))
            {
                //Act
                var repo = uow.GetReadOnlyRepository<TestProduct>();
                
                //Assert
                Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
              
            }
        }

        [Fact]
        public void ShouldReadFromProducts()
        {
            // Arrange 
            using (var uow = new UnitOfWork<TestDbContext>(_testFixture.Context))
            {
                var repo = uow.GetReadOnlyRepository<TestProduct>();

                //Act 
                var products = repo.GetList().Items;

                //Assert
                Assert.Equal(20, products.Count);
              
            }
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }
    }
}