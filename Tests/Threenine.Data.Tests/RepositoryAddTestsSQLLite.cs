using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("RepositoryAdd")]
    public class RepositoryAddTestsSqlLite 
    {
        public RepositoryAddTestsSqlLite(SqlLiteTestFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SqlLiteTestFixture _fixture;

        [Fact]
        public void ShouldAddNewCategory()
        {
            //arange 
            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestCategory>();
            var newCategory = new TestCategory {Name = GlobalTestStrings.TestProductCategoryName};

            //Act 
            repo.Add(newCategory);
            uow.SaveChanges();

            //Assert
            Assert.Equal(21, newCategory.Id);
        }

        [Fact]
        public void ShouldAddNewProduct()
        {
            //Arrange
            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            var newProduct = new TestProduct
            {
                Name = GlobalTestStrings.TestProductName,
                Category = new TestCategory {Id = 1, Name = GlobalTestStrings.TestProductCategoryName}
            };

            //Act 
            repo.Add(newProduct);
            uow.SaveChanges();

            //Assert
            Assert.Equal(21, newProduct.Id);
        }
    }
}