using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition("PagedList")]
    public class PagedListCollection : ICollectionFixture<SqlLiteTestFixture>
    {
        
    }
}