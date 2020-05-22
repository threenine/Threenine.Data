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
*/// Copyright (c) threenine.co.uk . All rights reserved.
//GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
//  See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class RepositoryAsyncTests : IDisposable
    {
        public RepositoryAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;


        [Fact]
        public async Task ShouldGet5ProductsAndIncludeCategoryInfo()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var results = await repo.GetListAsync(t => t.InStock == true && t.CategoryId == 1,
                include: category => category.Include(x => x.Category),
                size: 5);

            Assert.Equal(5, results.Items.Count);
            Assert.Equal(1, results.Pages);
            Assert.IsAssignableFrom<TestCategory>(results.Items[0].Category);
            Assert.Equal("Name1", results.Items[0].Category.Name);
        }

        [Fact]
        public async Task ShouldGetFiveProductsInStockOnePage()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var results = await repo.GetListAsync(t => t.InStock == true && t.CategoryId == 1,
                size: 5);

            Assert.Equal(5, results.Items.Count);
            Assert.Equal(1, results.Pages);
        }

        [Fact]
        public async Task ShouldGetListOf20TestProducts()
        {
            // Arrange 
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            // Act
            var testList = await repo.GetListAsync();

            //Assert
            Assert.NotNull(testList);
            Assert.IsAssignableFrom<IEnumerable<TestProduct>>(testList.Items);
            Assert.Equal(20, testList.Items.Count);
        }

        [Fact]
        public async Task ShouldGetSingleValueAsync()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();

            var product = await repo.SingleOrDefaultAsync(x => x.Id == 3);

            Assert.NotNull(product);
            Assert.IsAssignableFrom<TestProduct>(product);
        }
    }
}