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
    public class UpdateTests : IDisposable
    {
        private const string TestProductNameChange = "Test Product Name Change";
        private const string NewProductName = "Foo Bar";
        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateTests(SqlLiteWith20ProductsTestFixture fixture)
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

            var prod = await repo.SingleOrDefaultAsync(x => x.Id == 1);
            prod.Name = TestProductNameChange;

            var repo2 = _unitOfWork.GetRepository<TestProduct>();
            repo2.Update(prod);

            await _unitOfWork.CommitAsync();

            var prod2 = await repo.SingleOrDefaultAsync(x => x.Id == 1);

            prod2.Name.ShouldBeEquivalentTo(TestProductNameChange);
        }

        [Fact]
        public void ShouldThrowInvalidOperationException()
        {
            var repo = _unitOfWork.GetRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 1, enableTracking: false);

            product.ShouldBeAssignableTo<TestProduct>();

            product.Name = NewProductName;

            Should.Throw<InvalidOperationException>(() => repo.Update(product));
        }

        [Fact]
        public void ShouldUpdateMultipleProductsByParams()
        {
            const string newProduct1Name = "Foo Bar";
            const string newProduct2Name = "Bar Foo";

            var repo = _unitOfWork.GetRepository<TestProduct>();

            var product1 = repo.SingleOrDefault(x => x.Id == 1);
            var product2 = repo.SingleOrDefault(x => x.Id == 2);

            product1.Name = newProduct1Name;
            product2.Name = newProduct2Name;

            repo.Update(product1, product2);

            _unitOfWork.Commit();

            var updatedProduct1 = repo.SingleOrDefault(x => x.Id == 1);
            var updatedProduct2 = repo.SingleOrDefault(x => x.Id == 2);

            updatedProduct1.Name.ShouldBeEquivalentTo(newProduct1Name);
            updatedProduct2.Name.ShouldBeEquivalentTo(newProduct2Name);
        }

        [Fact]
        public void ShouldUpdateProductName()
        {
            const string newProductName = "Foo Bar";

            var repo = _unitOfWork.GetRepository<TestProduct>();

            var product = repo.SingleOrDefault(x => x.Id == 1);

            product.ShouldBeAssignableTo<TestProduct>();

            product.Name = newProductName;

            repo.Update(product);

            _unitOfWork.Commit();

            var updatedProduct = repo.SingleOrDefault(x => x.Id == 1);
            updatedProduct.Name.ShouldBeEquivalentTo(newProductName);
        }

        [Fact]
        public async Task ShouldUpdateWIthSameRepository()
        {
            var repo = _unitOfWork.GetRepository<TestProduct>();

            var prod = repo.SingleOrDefault(x => x.Id == 1);
            prod.Name = TestProductNameChange;

            repo.Update(prod);

            await _unitOfWork.CommitAsync();

            var prod2 = repo.SingleOrDefault(x => x.Id == 1);
            prod2.Name.ShouldBeEquivalentTo(TestProductNameChange);
        }
    }
}