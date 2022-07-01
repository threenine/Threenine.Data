using System;
using System.Threading.Tasks;
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.ReadOnlyRepositoryTests
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
        public async Task ShouldGetListOfItems()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var results = await repo.GetListAsync(t => t.InStock == true && t.CategoryId == 1,
                size: 5);
            
            results.ShouldSatisfyAllConditions(
                () => results.Items.Count.ShouldBeEquivalentTo(5),
                () => results.Pages.ShouldBeEquivalentTo(1)
                );
        }

        [Fact]
        public async Task ShouldGetSingleItem()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(predicate: x => x.Id == 1);

            product.ShouldNotBeNull();
           
        }

        [Fact]
        public void ShouldReturnInstanceIfInterface()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            Assert.IsAssignableFrom<IRepositoryReadOnlyAsync<TestProduct>>(repo);
        }

        [Fact]
        public async Task ShouldReturnNullObject()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(predicate: x => x.Id == 10001);

            Assert.Null(product);
        }
    }
}