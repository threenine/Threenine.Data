using TestDatabase;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection("ReadOnly")]
    public class ReadOnlyRepositoryTests
    {
        
        public ReadOnlyRepositoryTests(SqlLiteTestFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SqlLiteTestFixture _fixture;
        
        
        [Fact]
        public void ShouldBeReadOnlyInterface()
        {
            // Arrange 
            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();
             
            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
        }
        
       [Fact]
        public void ShouldReadFromProducts()
        {
            // Arrange 
            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();
             
            
            var products = repo.GetList().Items;
            
            Assert.Equal(20, products.Count);
        }
    }
}