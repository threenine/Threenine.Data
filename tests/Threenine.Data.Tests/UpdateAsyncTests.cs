using System;
using System.Threading.Tasks;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UpdateAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public UpdateAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldUpdateProductName()
        {
            const string newProductName = "Foo Bar";
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.IsAssignableFrom<TestProduct>(product);

            product.Name = newProductName;

              repo.Update(product);

           await uow.CommitAsync();

            var updatedProduct = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.Equal(updatedProduct.Name, newProductName);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}