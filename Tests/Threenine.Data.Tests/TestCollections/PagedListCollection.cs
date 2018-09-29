using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition("PagedList")]
    public class PagedListCollection : ICollectionFixture<SqlLiteWith20ProductsTestFixture>
    {
        
    }
}