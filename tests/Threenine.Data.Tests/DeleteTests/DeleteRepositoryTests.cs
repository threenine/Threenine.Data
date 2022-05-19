using System;
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.DeleteTests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class DeleteRepositoryTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public DeleteRepositoryTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        [Fact]
        public void Should_Delete_Product()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);

            var getRepo = uow.GetRepository<TestProduct>();
            var delRepo = uow.DeleteRepository<TestProduct>();


            var prod = getRepo.SingleOrDefault(x => x.Id == 1, enableTracking:true);
            delRepo.Delete(prod);
            uow.Commit();

            prod = getRepo.SingleOrDefault(x => x.Id == 1);

            prod.ShouldBeNull();
        }
    }
}