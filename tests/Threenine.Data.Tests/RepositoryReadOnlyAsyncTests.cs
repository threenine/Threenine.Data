using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class RepositoryReadOnlyAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;
        
        public RepositoryReadOnlyAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        [Fact]
        public void ShouldReturnInstanceIfInterface()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            Assert.IsAssignableFrom<IRepositoryReadOnlyAsync<TestProduct>>(repo);
        }
        
        [Fact]
        public async Task ShouldGetSingleItem()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(x => x.Id == 1);

            Assert.NotNull(product);
        }

        [Fact]
        public async Task ShouldGetListOfItems()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();
            
            var results = await repo.GetListAsync(t => t.InStock == true && t.CategoryId == 1,
                size: 5);

            Assert.Equal(5, results.Items.Count);
            Assert.Equal(1, results.Pages);
            
        }
        
        [Fact]
        public async Task ShouldReturnNullObject()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(x => x.Id == 10001);

            Assert.Null(product);
        }
    }
}