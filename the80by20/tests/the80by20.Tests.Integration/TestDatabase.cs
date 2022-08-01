using Microsoft.EntityFrameworkCore;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Tests.Integration;

internal sealed class TestDatabase : IDisposable
{
    public CoreDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<DatabaseOptions>("dataBase");
        Context = new CoreDbContext(new DbContextOptionsBuilder<CoreDbContext>().UseSqlServer(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}