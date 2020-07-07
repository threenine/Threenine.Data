using System;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UnitOfWorkTests : IDisposable
    {
        private static SqlLiteWith20ProductsTestFixture _testFixture;

        public UnitOfWorkTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        [Fact]
        public void GetOrAddReadOnlyRepositoryTests()
        {
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetOrAddRepository(typeof(TestProduct),
                new RepositoryReadOnly<TestProduct>(_testFixture.Context));
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
        }

        [Fact]
        public void GetOrAddRepositoryTests()
        {
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetOrAddRepository(typeof(TestProduct), new Repository<TestProduct>(_testFixture.Context));
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IRepository<TestProduct>>(repo);
        }

        [Fact]
        public void GetOrAddMultipleRepositoryTests()
        {
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetOrAddRepository(typeof(TestProduct), new Repository<TestProduct>(_testFixture.Context));
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IRepository<TestProduct>>(repo);

            var repo2 = uow.GetOrAddRepository(typeof(TestProduct),
                new RepositoryReadOnly<TestProduct>(_testFixture.Context));
            Assert.NotNull(repo2);
            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo2);
        }
    }
}