using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition("RepositoryAdd")]
    public class RepositoryAddCollection : ICollectionFixture<SqlLiteWithEmptyDataTestFixture>
    {
    }
}