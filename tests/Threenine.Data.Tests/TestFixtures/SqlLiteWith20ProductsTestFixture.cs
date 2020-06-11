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
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using TestDatabase;

namespace Threenine.Data.Tests.TestFixtures
{
    public class SqlLiteWith20ProductsTestFixture : IDisposable
    {
        public TestDbContext Context => SqlLiteInMemoryContext();

        public void Dispose()
        {
            Context?.Dispose();
        }

        private TestDbContext SqlLiteInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.TestCategories.AddRange(TestCategories());
            context.TestProducts.AddRange(TestProducts());
            context.SaveChanges();
            return context;
        }

        private List<TestCategory> TestCategories()
        {
            BuilderSetup.DisablePropertyNamingFor<TestCategory, int>(x => x.Id);
            return Builder<TestCategory>.CreateListOfSize(20).Build().ToList();
        }

        private List<TestProduct> TestProducts()
        {
            BuilderSetup.DisablePropertyNamingFor<TestProduct, int>(x => x.Id);
            var productList = Builder<TestProduct>.CreateListOfSize(20)
                .TheFirst(5)
                .With(x => x.CategoryId = 1)
                .With(x => x.InStock = true)
                .With(x => x.Stock = 300)
                .TheNext(5)
                .With(x => x.InStock = false)
                .With(x => x.CategoryId = 2)
                .With(y => y.Stock = 0)
                .Build();

            return productList.ToList();
        }
    }
}