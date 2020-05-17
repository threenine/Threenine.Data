using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("PagedList")]
    public class InsertAsyncTests: IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public InsertAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

       [Fact]
        public async Task ShouldInsertNewProduct()
        {
            var prod = Builder<TestProduct>.CreateNew().With(x => x.Name = "Cool Product").With(x=> x.Id = 1001).With(x=> x.CategoryId = 1).Build();
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            await repo.InsertAsync(prod);
            uow.SaveChanges();

            var getNewProd = await repo.SingleOrDefaultAsync(x => x.Name == "Cool Product");
            
            Assert.NotNull(getNewProd);
            Assert.IsAssignableFrom<TestProduct>(getNewProd);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}