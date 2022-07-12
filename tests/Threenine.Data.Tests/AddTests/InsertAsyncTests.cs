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
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.AddTests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class InsertAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        public InsertAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        [Fact]
        public async Task ShouldInsertNewProductndReturnCreatedEntity()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var prod = Builder<TestProduct>.CreateNew().With(x => x.Name = "Cool Product").With(x => x.CategoryId = 1)
                .Build();
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);

            var repo = uow.GetRepositoryAsync<TestProduct>();

            var newProduct = await repo.InsertAsync(prod);
            await uow.CommitAsync();

            newProduct.ShouldSatisfyAllConditions(
                () => newProduct.ShouldNotBeNull(),
                () => newProduct.Entity.ShouldBeOfType<TestProduct>()
            );
          
        }
    }
}