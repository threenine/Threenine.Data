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
    public class DeleteTests : IDisposable
    {
        public DeleteTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }


        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public void ShouldDeleteProduct()
        {
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);

            var get = uow.GetRepository<TestProduct>();


            uow.Commit();

            var prod = get.SingleOrDefault(x => x.Id == 1);
            get.Delete(prod);
            uow.Commit();

            prod = get.SingleOrDefault(x => x.Id == 1);
            Assert.Null(prod);
        }
    }
}