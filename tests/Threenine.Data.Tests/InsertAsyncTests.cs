using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class InsertAsyncTests: IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public InsertAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

       [Fact]
        public async Task ShouldInsertNewProductndReturnCreatedEntity()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew().With(x => x.Name = "Cool Product").With(x=> x.CategoryId = 1).Build();
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var newProduct = await repo.InsertAsync(prod);
            await uow.CommitAsync();

           
            
            Assert.NotNull(newProduct);
            Assert.IsAssignableFrom<TestProduct>(newProduct.Entity);
            Assert.Equal(21, newProduct.Entity.Id);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}