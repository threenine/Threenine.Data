using System;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UpdateTests: IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public UpdateTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldUpdateProductName()
        {
            const string newProductName = "Foo Bar";
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product = repo.GetSingleOrDefault(x => x.Id == 1 );

            Assert.IsAssignableFrom<TestProduct>(product);
            
            product.Name = newProductName;
            
            repo.Update(product);
           
            uow.Commit();

           var updatedProduct = repo.GetSingleOrDefault(x => x.Id == 1);
           
           Assert.Equal(updatedProduct.Name, newProductName);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
        
        
    }
}