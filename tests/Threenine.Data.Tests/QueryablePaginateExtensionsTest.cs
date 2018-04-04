using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Sample.Entity;
using TestDatabase;
using Threenine.Data.Paging;
using Xunit;

namespace Threenine.Data.Tests
{
   public class QueryablePaginateExtensionsTest : IClassFixture<InMemoryTestFixture>
    {
        private readonly InMemoryTestFixture _fixture;

        public QueryablePaginateExtensionsTest(InMemoryTestFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task ToPaginateAsyncTest()
        {

            var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepositoryAsync<TestProduct>();
            
            await  repo.AddAsync(TestProducts());
            uow.SaveChanges();

            var products = await repo.GetListAsync();

           var page=  products.Items.ToPaginate(1, 2);

            Assert.NotNull(page);
            Assert.Equal(20, page.Count);
            Assert.Equal(10, page.Pages);
            Assert.Equal(2, page.Size);

        }



        private List<TestProduct> TestProducts()
        {
            var testProducts = Builder<TestProduct>.CreateListOfSize(35).Build();
            return testProducts.ToList();
        }
    }
}
