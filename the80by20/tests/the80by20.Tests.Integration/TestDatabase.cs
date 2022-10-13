
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Shared.Infrastucture;
using the80by20.Solution.Infrastructure.EF;
using the80by20.Users.Infrastructure.EF;

namespace the80by20.Tests.Integration;


internal interface IWithCoreDbContext : IDisposable
{
    SolutionDbContext SolutionDbContext { get; }
    MasterDataDbContext MasterDataDbContext { get; }
    UsersDbContext UsersDbContext { get; }
}

internal sealed class TestDatabase : IWithCoreDbContext
{
    public SolutionDbContext SolutionDbContext { get; }
    public MasterDataDbContext MasterDataDbContext { get; }
    public UsersDbContext UsersDbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<DatabaseOptions>("dataBase");

        SolutionDbContext = new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>().UseSqlServer(options.ConnectionString).Options);
        MasterDataDbContext = new MasterDataDbContext(new DbContextOptionsBuilder<MasterDataDbContext>().UseSqlServer(options.ConnectionString).Options);
        UsersDbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().UseSqlServer(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        SolutionDbContext.Database.EnsureDeleted();
        SolutionDbContext.Dispose();

        MasterDataDbContext.Database.EnsureDeleted();
        MasterDataDbContext.Dispose();

        UsersDbContext.Database.EnsureDeleted();
        UsersDbContext.Dispose();
    }
}

internal sealed class TestSqlLiteInMemoryDatabase : IWithCoreDbContext
{
    public SolutionDbContext SolutionDbContext { get; }
    public MasterDataDbContext MasterDataDbContext { get; }
    public UsersDbContext UsersDbContext { get; }

    public TestSqlLiteInMemoryDatabase(SqliteConnection connection)
    {
        //https://docs.microsoft.com/en-us/ef/core/testing/testing-without-the-database#sqlite-in-memory
        //https://stackoverflow.com/questions/58375527/override-ef-core-dbcontext-in-asp-net-core-webapplicationfactory
        SolutionDbContext = new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>().UseSqlite(connection).Options);
        SolutionDbContext.Database.EnsureCreated();

        MasterDataDbContext = new MasterDataDbContext(new DbContextOptionsBuilder<MasterDataDbContext>().UseSqlite(connection).Options);
        MasterDataDbContext.Database.EnsureCreated();

        UsersDbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().UseSqlite(connection).Options);
        UsersDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        SolutionDbContext.Database.EnsureDeleted();
        SolutionDbContext.Dispose();

        MasterDataDbContext.Database.EnsureDeleted();
        MasterDataDbContext.Dispose();

        UsersDbContext.Database.EnsureDeleted();
        UsersDbContext.Dispose();
    }
}