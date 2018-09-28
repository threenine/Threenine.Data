using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TestDatabase;
using Xunit;
using FizzWare.NBuilder;

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
        public void GetProductPagedListTest()
        {
            //Arrange 
            var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(predicate: x => x.CategoryId == 1).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }

       
    }
}