using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.GetTests
{
    [Collection(GlobalTestStrings.Product40COllection)]
    public class GetSingleTests : IDisposable
    {
        private readonly IRepository<TestProduct> _repository;

        private readonly SqlLiteWith40ProductsTestFixture _testFixture;
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleTests(SqlLiteWith40ProductsTestFixture fixture)
        {
            _testFixture = fixture;
            _unitOfWork = new UnitOfWork<TestDbContext>(fixture.Context);
            _repository = _unitOfWork.GetRepository<TestProduct>();
        }
        [Fact]
        public void GetProductSingleOrDefaultOrderbyTest()
        {
            //Act
            var product = _unitOfWork.GetRepository<TestProduct>().SingleOrDefault(
                orderBy: x => x.OrderBy(product => product.Name),
                include: x => x.Include(cat => cat.Category));
            //Assert
            product.ShouldNotBeNull();
            product.Name.ShouldBeEquivalentTo("Name1");
            product.ShouldBeAssignableTo<TestProduct>();
            product.Category.ShouldBeAssignableTo<TestCategory>();
        }

        [Fact]
        public void GetProductSingleOrDefaultTest()
        {
            //Act
            var product = _unitOfWork.GetRepository<TestProduct>().SingleOrDefault(x => x.Id == 1);
            //Assert
            product.ShouldNotBeNull();
            product.Id.ShouldBeEquivalentTo(1);
        }
        
        
        public void Dispose()
        {
            _repository?.Dispose();
            _unitOfWork?.Dispose();
            _testFixture?.Dispose();
        }
    }
}