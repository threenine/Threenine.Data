using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("PagedList")]
    public class RepositoryAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public RepositoryAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldGetListOf20TestProducts()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            // Act
            var testList = await repo.GetListAsync();

            //Assert
            Assert.NotNull(testList);
            Assert.IsAssignableFrom<IEnumerable<TestProduct>>(testList.Items);
            Assert.Equal(20, testList.Items.Count);
        }
        
        [Fact]
        public async Task ShouldGetFiveProductsInStockOnePage()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var results = await repo.GetListAsync( t => t.InStock == true && t.CategoryId == 1,
                size: 5);

            Assert.Equal(5, results.Items.Count);
            Assert.Equal(1, results.Pages);
        }
        
         
        [Fact]
        public async Task ShouldGet5ProductsAndIncludeCategoryInfo()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var results = await repo.GetListAsync( t => t.InStock == true && t.CategoryId == 1,
                include: category => category.Include(x => x.Category),
                size: 5);

            Assert.Equal(5, results.Items.Count);
            Assert.Equal(1, results.Pages);
            Assert.IsAssignableFrom<TestCategory>(results.Items[0].Category);
            Assert.Equal("Name1",results.Items[0].Category.Name);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}