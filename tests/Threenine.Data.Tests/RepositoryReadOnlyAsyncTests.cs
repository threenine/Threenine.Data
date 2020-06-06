using System;
using System.Threading.Tasks;
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
        public async Task ShouldGetItems()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(x => x.Id == 1);

            Assert.NotNull(product);
        }
    }
}