using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests.TestCollections
{
    [CollectionDefinition("ReadOnly")]
    public class ReadOnlyRepositoryCollection : ICollectionFixture<SqlLiteReadOnlyTestFixture>
    {
    }
}