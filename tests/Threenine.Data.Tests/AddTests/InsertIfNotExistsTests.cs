using System;
using FizzWare.NBuilder;
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.AddTests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class InsertIfNotExistsTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public InsertIfNotExistsTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldInsertNewProductBecauseNotExist()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew()
                .With(x => x.Name = "Cool Product")
                .With(x => x.CategoryId = 1)
                .Build();

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var newProduct = repo.InsertNotExists(x => x.CategoryId == 1 && x.Name == "Cool Product", prod);
            uow.Commit();
            newProduct.ShouldSatisfyAllConditions(
                () => newProduct.ShouldNotBeNull(),
                () => newProduct.ShouldBeAssignableTo<TestProduct>(),
                () => newProduct.Id.ShouldBeEquivalentTo(21)
            );

          
        }

        [Fact]
        public void ShouldReturnProductBecauseNExist()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew()
                .With(x => x.Name = "Name1")
                .With(x => x.CategoryId = 1)
                .Build();

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var newProduct = repo.InsertNotExists(x => x.CategoryId == 1 && x.Name == "Name1", prod);
            uow.Commit();

            newProduct.ShouldSatisfyAllConditions(
                () => newProduct.ShouldNotBeNull(),
                () => newProduct.ShouldBeAssignableTo<TestProduct>(),
                () => newProduct.Id.ShouldBeEquivalentTo(1)
            );
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}