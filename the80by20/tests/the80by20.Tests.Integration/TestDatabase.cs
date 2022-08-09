﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Tests.Integration;


internal interface IWithCoreDbContext : IDisposable
{
    CoreDbContext Context { get; }
}

internal sealed class TestDatabase : IWithCoreDbContext
{
    public CoreDbContext Context { get; }

    // todo check in memory sqllite
    // todo run tests against db in ci
    // - inmemory
    // - using docker image with sql server
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

// todo
internal sealed class TestSqlLiteInMemoryDatabase : IWithCoreDbContext
{
    public CoreDbContext Context { get; }

    public TestSqlLiteInMemoryDatabase(SqliteConnection connection)
    {
        //https://docs.microsoft.com/en-us/ef/core/testing/testing-without-the-database#sqlite-in-memory
        //https://stackoverflow.com/questions/58375527/override-ef-core-dbcontext-in-asp-net-core-webapplicationfactory
        Context = new CoreDbContext(new DbContextOptionsBuilder<CoreDbContext>().UseSqlite(connection).Options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}