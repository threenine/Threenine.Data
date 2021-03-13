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
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.GetTests
{
    [Collection(GlobalTestStrings.Product40COllection)]
    public class GetRepositoryTests : IDisposable
    {
        private readonly IRepository<TestProduct> _repository;

        private readonly SqlLiteWith40ProductsTestFixture _testFixture;
        private readonly IUnitOfWork _unitOfWork;

        public GetRepositoryTests(SqlLiteWith40ProductsTestFixture fixture)
        {
            _testFixture = fixture;
            _unitOfWork = new UnitOfWork<TestDbContext>(fixture.Context);
            _repository = _unitOfWork.GetRepository<TestProduct>();
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        [Fact]
        public void GetProductPagedListPaginate()
        {
            //Act
            var productList = _repository.GetList(size: 5);
            //Assert
            productList.Items.Count.ShouldBeEquivalentTo(5);
            productList.Pages.ShouldBeEquivalentTo(8);
            productList.Size.ShouldBeEquivalentTo(5);
            productList.HasNext.ShouldBeTrue();
        }

        [Fact]
        public void GetProductPagedListUsingWithNoPredicateTest()
        {
            //Act
            var productList = _repository.GetList(size: int.MaxValue).Items;
            //Assert
            productList.Count.ShouldBeEquivalentTo(40);
        }

        [Fact]
        public void GetProductPagedListWith8PagesTest()
        {
            //Act
            var productList = _repository.GetList(size: 5);
            //Assert
            productList.Items.Count.ShouldBeEquivalentTo(5);
            productList.Pages.ShouldBeEquivalentTo(8);
        }

        [Fact]
        public void GetProductPagedListWithPredicateTest()
        {
            //Act
            var productList = _repository.GetList(x => x.CategoryId == 1);
            //Assert
            productList.Items.Count.ShouldBeEquivalentTo(5);
            productList.Pages.ShouldBeEquivalentTo(1);
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
    }
}