using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using TestDatabase;
using Xunit;
namespace Threenine.Data.Tests
{
    public class PaginateTests : IClassFixture<InMemoryTestFixture>
    {
        private readonly InMemoryTestFixture _fixture;

        public PaginateTests(InMemoryTestFixture fixture)
        {
         
            _fixture = fixture;

            

        }

        [Fact]
        public void GetPaginate()
        {
            // Arrange 
            _fixture.Context.TestProducts.AddRange(TestProducts());
            _fixture.Context.TestCategories.AddRange(TestCategories());
            _fixture.Context.SaveChanges();

            //var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var prodRepo = new Repository<TestProduct>(_fixture.Context);  //uow.GetRepository<TestProduct>();
          //  var catRepo = uow.GetRepository<TestCategory>();


            //prodRepo.Add(TestProducts());
            //catRepo.Add(TestCategories());
            //uow.SaveChanges();

            var page = prodRepo.GetList(predicate: t => t.Name == GlobalTestStrings.TestProductName,
                include: source => source.Include(t => t.Category), size: 1);
            Assert.Equal(1, page.Items.Count);
            Assert.NotNull(page.Items[0].Category);




        }



        private List<TestProduct> TestProducts()
        {
            var testProducts = Builder<TestProduct>.CreateListOfSize(35)
                .TheFirst(1)
                .With(x => x.Name = GlobalTestStrings.TestProductName)
                .With(x => x.Category = new TestCategory(){Name = GlobalTestStrings.TestProductCategoryName, Id = 1})
                .Build();
            return testProducts.ToList();
        }

        private List<TestCategory> TestCategories()
        {
            var testCategories = Builder<TestCategory>.CreateListOfSize(5)
                .TheFirst(1)
                .With(x => x.Name = GlobalTestStrings.TestProductCategoryName)
                .Build();
            return testCategories.ToList();
        }
    }
}
