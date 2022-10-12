using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Shared.Infrastucture;
using the80by20.Solution.Infrastructure.EF;

namespace the80by20.Tests.Integration;


internal interface IWithCoreDbContext : IDisposable
{
    SolutionDbContext Context { get; }
    MasterDataDbContext MasterDataDbContext { get; }
}

internal sealed class TestDatabase : IWithCoreDbContext
{
    public SolutionDbContext Context { get; }
    public MasterDataDbContext MasterDataDbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<DatabaseOptions>("dataBase");
        Context = new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>().UseSqlServer(options.ConnectionString).Options);
        MasterDataDbContext = new MasterDataDbContext(new DbContextOptionsBuilder<MasterDataDbContext>().UseSqlServer(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();

        MasterDataDbContext.Database.EnsureDeleted();
        MasterDataDbContext.Dispose();
    }
}

internal sealed class TestSqlLiteInMemoryDatabase : IWithCoreDbContext
{
    public SolutionDbContext Context { get; }
    public MasterDataDbContext MasterDataDbContext { get; }

    public TestSqlLiteInMemoryDatabase(SqliteConnection connection)
    {
        //https://docs.microsoft.com/en-us/ef/core/testing/testing-without-the-database#sqlite-in-memory
        //https://stackoverflow.com/questions/58375527/override-ef-core-dbcontext-in-asp-net-core-webapplicationfactory
        Context = new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>().UseSqlite(connection).Options);
        Context.Database.EnsureCreated();

        MasterDataDbContext = new MasterDataDbContext(new DbContextOptionsBuilder<MasterDataDbContext>().UseSqlite(connection).Options);
        MasterDataDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();

        MasterDataDbContext.Database.EnsureDeleted();
        MasterDataDbContext.Dispose();
    }
}