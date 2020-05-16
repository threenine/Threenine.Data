using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("PagedList")]
    public class RepositoryAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public RepositoryAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldGetListOf20TestProducts()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            // Act
            var testList = await repo.GetListAsync();

            //Assert
            Assert.NotNull(testList);
            Assert.IsAssignableFrom<IEnumerable<TestProduct>>(testList.Items);
            Assert.Equal(20, testList.Items.Count);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}