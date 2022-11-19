using the80by20.Modules.Users.Infrastructure.EF;
using the80by20.Tests.Shared;

namespace the80by20.Tests.Integration.Common
{
    public class TestUsersDbContext : IDisposable
    {
        public UsersDbContext DbContext { get; }

        public TestUsersDbContext()
        {
            DbContext = new UsersDbContext(DbHelper.GetOptions<UsersDbContext>());
        }

        public void Dispose()
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}
