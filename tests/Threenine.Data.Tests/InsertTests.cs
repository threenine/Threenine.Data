// Copyright (c) threenine.co.uk . All rights reserved.
//GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
//  See LICENSE in the project root for license information.
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
using FizzWare.NBuilder;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class InsertTests : IDisposable
    {
        public InsertTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public void ShouldInsertAndReturnCreatedEntity()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew()
                .With(x => x.Name = "Cool Product")
                .With(x => x.CategoryId = 1)
                .Build();

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var newProduct = repo.Insert(prod);
            uow.Commit();

            Assert.NotNull(newProduct);
            Assert.IsAssignableFrom<TestProduct>(newProduct);
            Assert.Equal(21, newProduct.Id);
        }

        [Fact]
        public void ShouldInsertMultipleProductsByList()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var products = Builder<TestProduct>.CreateListOfSize(3)
                .TheFirst(3)
                .With(x => x.CategoryId = 1)
                .Build();

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            repo.Insert(products);
            uow.Commit();
            var numberOfItems = repo.GetList().Count;

            Assert.Equal(23, numberOfItems);
        }

        [Fact]
        public void ShouldInsertMutipleProductsByParams()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);

            var product1 = Builder<TestProduct>.CreateNew().Build();
            var product2 = Builder<TestProduct>.CreateNew().Build();

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            repo.Insert(product1, product2);

            uow.Commit();

            var numberOfItems = repo.GetList().Count;

            Assert.Equal(22, numberOfItems);
        }
    }
}