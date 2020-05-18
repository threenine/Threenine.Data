using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition(GlobalTestStrings.ProductCollectionName)]
    public class ProductCollection : ICollectionFixture<SqlLiteWith20ProductsTestFixture>
    {
    }
}