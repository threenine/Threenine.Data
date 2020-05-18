using System;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("RepositoryAdd")]
    public class RepositoryAddTestsSqlLite : IDisposable
    {
        public RepositoryAddTestsSqlLite(SqlLiteWithEmptyDataTestFixture fixture)
        {
            _fixture = fixture;
        }


        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWithEmptyDataTestFixture _fixture;

        [Fact]
        public void ShouldAddNewCategory()
        {
            //arange 
            using (var uow = new UnitOfWork<TestDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<TestCategory>();
                var newCategory = new TestCategory {Name = GlobalTestStrings.TestProductCategoryName};
                //Act 
                repo.Insert(newCategory);
                uow.Commit();
                //Assert
                Assert.Equal(1, newCategory.Id);
            }
        }

        [Fact]
        public void ShouldAddNewProduct()
        {
            //Arrange
            using (var uow = new UnitOfWork<TestDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<TestProduct>();
                var newProduct = new TestProduct
                {
                    Name = GlobalTestStrings.TestProductName,
                    Category = new TestCategory {Id = 1, Name = GlobalTestStrings.TestProductCategoryName}
                };

                //Act 
                 repo.Insert(newProduct);
                uow.Commit();

                
                //Assert
                Assert.Equal(1, newProduct.Id);
            }
        }
    }
}