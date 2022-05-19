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
using Shouldly;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.UpdateTests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UpdateAsyncTests : IDisposable
    {
        private readonly SqlLiteWith20ProductsTestFixture _fixture;
        private readonly IUnitOfWork _unitOfWork;
        private const string TestProductNameChange = "Test Product Name Change";
        public UpdateAsyncTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
            _unitOfWork = new UnitOfWork<TestDbContext>(fixture.Context);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _fixture?.Dispose();
        }
        
        [Fact]
        public async Task ShouldAddMultipleRepositoryTypes()
        {
            var repo = _unitOfWork.GetRepositoryAsync<TestProduct>();

            var prod = await repo.SingleOrDefaultAsync(x => x.Id == 1, enableTracking:true );
            prod.Name = TestProductNameChange;

            var repo2 = _unitOfWork.GetRepository<TestProduct>();
            repo2.Update(prod);

            await _unitOfWork.CommitAsync();

            var prod2 = await repo.SingleOrDefaultAsync(x => x.Id == 1, enableTracking:true);

            prod2.Name.ShouldBeEquivalentTo(TestProductNameChange);
        }

       
    }
}