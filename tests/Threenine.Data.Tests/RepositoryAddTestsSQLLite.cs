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
    [Collection("RepositoryAdd")]
    public class RepositoryAddTestsSqlLite : IDisposable
    {
        public RepositoryAddTestsSqlLite(SqlLiteWithEmptyDataTestFixture fixture)
        {
            _fixture = fixture;
        }


        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWithEmptyDataTestFixture _fixture;

        [Fact]
        public void ShouldAddNewCategory()
        {
            //arange 
            using (var uow = new UnitOfWork<TestDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<TestCategory>();
                var newCategory = new TestCategory {Name = GlobalTestStrings.TestProductCategoryName};
                //Act 
                repo.Insert(newCategory);
                uow.Commit();
                //Assert
                Assert.Equal(1, newCategory.Id);
            }
        }

        [Fact]
        public void ShouldAddNewProduct()
        {
            //Arrange
            using (var uow = new UnitOfWork<TestDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<TestProduct>();
                var newProduct = new TestProduct
                {
                    Name = GlobalTestStrings.TestProductName,
                    Category = new TestCategory {Id = 1, Name = GlobalTestStrings.TestProductCategoryName}
                };

                //Act 
                repo.Insert(newProduct);
                uow.Commit();


                //Assert
                Assert.Equal(1, newProduct.Id);
            }
        }
    }
}