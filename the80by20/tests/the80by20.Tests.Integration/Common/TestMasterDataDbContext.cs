using the80by20.Modules.Masterdata.Infrastructure.EF;
using the80by20.Tests.Shared;

namespace the80by20.Tests.Integration.Common
{
    public class TestMasterDataDbContext : IDisposable
    {
        public MasterDataDbContext DbContext { get; }

        public TestMasterDataDbContext()
        {
            DbContext = new MasterDataDbContext(DbHelper.GetOptions<MasterDataDbContext>());
        }

        public void Dispose()
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}
