using System;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class ReadOnlyRepositoryTests : IDisposable
    {
        public ReadOnlyRepositoryTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public void ShouldGetItems()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 1);

            Assert.NotNull(product);
        }

        [Fact]
        public void ShouldReturnNullObject()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 10001);

            Assert.Null(product);
        }
    }
}