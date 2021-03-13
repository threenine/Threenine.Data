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
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.GetTests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class GetPagedListTest : IDisposable
    {
        private readonly IRepository<TestProduct> _repository;

        private readonly SqlLiteWith20ProductsTestFixture _testFixture;
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedListTest(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
            _unitOfWork = new UnitOfWork<TestDbContext>(fixture.Context);
            _repository = _unitOfWork.GetRepository<TestProduct>();
        }

        public void Dispose()
        {
            _repository?.Dispose();
            _unitOfWork?.Dispose();
            _testFixture?.Dispose();
        }

        [Fact]
        public void Should_Get_Product_Paged_List_Using_Predicate()
        {
            //Act
            var productList = _repository.GetList(x => x.CategoryId == 1).Items;
            //Assert
            productList.Count.ShouldBeEquivalentTo(5);
        }

        [Fact]
        public void Should_Return_Read_Only_Interface()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_testFixture.Context);
            //Act
            var repo = uow.GetReadOnlyRepository<TestProduct>();
            //Assert
            repo.ShouldBeAssignableTo<IRepositoryReadOnly<TestProduct>>();
        }

        [Fact]
        public void Should_Get_5_Products_Out_Of_Stock_Multi_Predicate()
        {
            //Act
            var productList = _repository.GetList(x => x.Stock == 0 && x.InStock.Value == false).Items;

            //Assert
            productList.Count.ShouldBeEquivalentTo(5);
        }

        [Fact]
        public void Should_Read_From_Products()
        {
            //Act 
            var products = _repository.GetList().Items;
            //Assert
            products.Count.ShouldBeEquivalentTo(20);
        }

        [Fact]
        public void ShouldGetListWithSelectedColumns()
        {
            //Act
            var list = _repository.GetList(s => new
            {
                ProductName = s.Name,
                StockLevel = s.Stock
            });

            //Assert
            list.ShouldNotBeNull();
            list.Items.Count.ShouldBeEquivalentTo(20);
        }
    }
}