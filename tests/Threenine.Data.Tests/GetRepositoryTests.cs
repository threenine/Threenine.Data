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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.Product40COllection)]
    public class GetRepositoryTests : IDisposable
    {
        public GetRepositoryTests(SqlLiteWith40ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        private readonly SqlLiteWith40ProductsTestFixture _testFixture;

        [Fact]
        public void GetProductPagedListPaginate()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(size: 5);
            //Assert
            Assert.Equal(5, productList.Items.Count);
            Assert.Equal(8, productList.Pages);
            Assert.Equal(5, productList.Size);
            Assert.True(productList.HasNext);
        }

        [Fact]
        public void GetProductPagedListUsingWithNoPredicateTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(size: int.MaxValue).Items;
            //Assert
            Assert.Equal(40, productList.Count);
        }

        [Fact]
        public void GetProductPagedListWith8PagesTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(size: 5);
            //Assert
            Assert.Equal(5, productList.Items.Count);
            Assert.Equal(8, productList.Pages);
        }

        [Fact]
        public void GetProductPagedListWithPredicateTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            var repo = uow.GetRepository<TestProduct>();
            //Act
            var productList = repo.GetList(x => x.CategoryId == 1);
            //Assert
            Assert.Equal(5, productList.Items.Count);
            Assert.Equal(1, productList.Pages);
        }

        [Fact]
        public void GetProductSingleOrDefaultOrderbyTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);

            //Act
            var product = uow.GetRepository<TestProduct>().SingleOrDefault(orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Category));
            //Assert
            Assert.NotNull(product);
            Assert.Equal("Name1", product.Name);
            //Assert.Equal(1, product.Id);
        }

        [Fact]
        public void GetProductSingleOrDefaultTest()
        {
            //Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);

            //Act
            var product = uow.GetRepository<TestProduct>().SingleOrDefault(x => x.Id == 1);
            //Assert
            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
        }
    }
}