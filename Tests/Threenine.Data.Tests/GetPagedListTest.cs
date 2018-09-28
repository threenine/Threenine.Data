using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TestDatabase;
using Xunit;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data.Tests
{
   [Collection("PagedList")]
    public class GetPagedListTest 
    {
        private readonly SqlLiteTestFixture _testFixture;
        public GetPagedListTest(SqlLiteTestFixture fixture)
        {
            _testFixture = fixture;
        }
       
        
        [Fact]
        public void GetProductPagedListUsingPredicateTest()
        {
            //Arrange 
            var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(predicate: x => x.CategoryId == 1).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }

        [Fact]
        public void GetPagedListIncludesTest()
        {
            var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);

            var cats = uow.GetRepository<TestCategory>().GetList(include: source =>
                source.Include(x => x.Products).ThenInclude(prod => prod.Category), size:5);
            
            Assert.IsAssignableFrom<Paging.Paginate<TestCategory>>(cats);
            
            Assert.Equal(cats.Count, 20);
            Assert.Equal(cats.Pages, 4);
            Assert.Equal(cats.Items.Count, 5);
            
        }

       
    }
}