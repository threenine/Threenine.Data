using System;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("PagedList")]
    public class InsertTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public InsertTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShoudInsertandReturnCreatedEntity()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew()
                .With(x => x.Name = "Cool Product")
                .With(x => x.CategoryId = 1)
                .Build();
            
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

             var newProduct  = repo.Insert(prod);
            uow.SaveChanges();
            
            Assert.NotNull(newProduct);
            Assert.IsAssignableFrom<TestProduct>(newProduct);
            
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}