// Copyright (c) threenine.co.uk . All rights reserved.
//GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
//  See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class InsertAsyncTests : IDisposable
    {
        public InsertAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public async Task ShouldInsertNewProductndReturnCreatedEntity()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew().With(x => x.Name = "Cool Product").With(x => x.CategoryId = 1)
                .Build();
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);

            var repo = uow.GetRepositoryAsync<TestProduct>();

            var newProduct = await repo.InsertAsync(prod);
            await uow.CommitAsync();

            Assert.NotNull(newProduct);
            Assert.IsAssignableFrom<TestProduct>(newProduct.Entity);
            Assert.Equal(21, newProduct.Entity.Id);
        }
    }
}