using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
    public class RepositoryAddTest : IClassFixture<InMemoryTestFixture>
    {
        public RepositoryAddTest(InMemoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly InMemoryTestFixture _fixture;

        [Fact]
        public void ShouldAddNewProduct()
        {
            // Arrange 
            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            var newProduct = new TestProduct {Name = GlobalTestStrings.TestProductName};

            // Act
            repo.Add(newProduct);
            uow.SaveChanges();

            //Assert
            Assert.Equal(1, newProduct.Id);
        }
    }
}