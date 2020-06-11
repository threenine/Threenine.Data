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
using System.Collections.Generic;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class RepositoryReadOnlyTests : IDisposable
    {
        public RepositoryReadOnlyTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public void ShouldGetItems()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 1);

            Assert.NotNull(product);
        }

        [Fact]
        public void ShouldGetListOfProducts()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var products = repo.GetList();

            Assert.NotNull(products);
        }

        [Fact]
        public void ShouldReturnAListOfProducts()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var products = repo.GetList(x => x.CategoryId == 1);

            Assert.NotNull(products);
            Assert.IsAssignableFrom<IEnumerable<TestProduct>>(products.Items);
        }

        [Fact]
        public void ShouldReturnInstanceIfInterface()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            Assert.IsAssignableFrom<IRepositoryReadOnly<TestProduct>>(repo);
        }

        [Fact]
        public void ShouldReturnNullObject()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetReadOnlyRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 10001);

            Assert.Null(product);
        }
    }
}