using the80by20.Modules.Solution.Infrastructure.EF;
using the80by20.Tests.Shared;

namespace the80by20.Tests.Integration.Common
{
    public class TestSolutionDbContext : IDisposable
    {
        public SolutionDbContext DbContext { get; }

        public TestSolutionDbContext()
        {
            DbContext = new SolutionDbContext(DbHelper.GetOptions<SolutionDbContext>());
        }

        public void Dispose()
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}
