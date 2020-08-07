using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition(GlobalTestStrings.Product40COllection)]
    public class Product40Collection : ICollectionFixture<SqlLiteWith40ProductsTestFixture>
    {
    }
}