using System;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;
using Xunit.Sdk;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UpdateTests : IDisposable
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

            var product = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.IsAssignableFrom<TestProduct>(product);

            product.Name = newProductName;

            repo.Update(product);

            uow.Commit();

            var updatedProduct = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.Equal(updatedProduct.Name, newProductName);
        }

        [Fact]
        public void ShouldUpdateMultipleProductsByParams()
        {
            const string newProduct1Name = "Foo Bar";
            const string newProduct2Name = "Bar Foo";

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product1 = repo.GetSingleOrDefault(x => x.Id == 1);
            var product2 = repo.GetSingleOrDefault(x => x.Id == 2);


            product1.Name = newProduct1Name;
            product2.Name = newProduct2Name;

            repo.Update(product1, product2);

            uow.Commit();

            var updatedProduct1 = repo.GetSingleOrDefault(x => x.Id == 1);
            var updatedProduct2 = repo.GetSingleOrDefault(x => x.Id == 2);
            Assert.Equal(updatedProduct1.Name, newProduct1Name);
            Assert.Equal(updatedProduct2.Name, newProduct2Name);
        }

        [Fact]
        public void ShouldThrowInvalidOperationException()
        {
            const string newProductName = "Foo Bar";
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product = repo.GetSingleOrDefault(x => x.Id == 1, enableTracking: false);

            Assert.IsAssignableFrom<TestProduct>(product);

            product.Name = newProductName;

            Assert.Throws<InvalidOperationException>(() => repo.Update(product));
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}