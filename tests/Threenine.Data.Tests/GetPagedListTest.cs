/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class GetPagedListTest : IDisposable
    {
        public GetPagedListTest(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _testFixture;

        [Fact]
        public void GetProductPagedListUsingPredicateTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(x => x.CategoryId == 1).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }

        [Fact]
        public void ShouldBeReadOnlyInterface()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            //Act
            var repo = uow.GetReadOnlyRepository<TestProduct>();
            //Assert
            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
        }

        [Fact]
        public void ShouldGet5ProductsOutOfStockMultiPredicateTest()
        {
            // Arrange
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(x => x.Stock == 0 && x.InStock.Value == false).Items;
            //Assert
            Assert.Equal(5, productList.Count);
        }

        [Fact]
        public void ShouldReadFromProducts()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();
            //Act 
            var products = repo.GetList().Items;
            //Assert
            Assert.Equal(20, products.Count);
        }
    }
}