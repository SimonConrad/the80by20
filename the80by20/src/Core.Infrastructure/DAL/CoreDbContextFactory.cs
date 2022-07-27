using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL;

public static class CoreDbContextFactory
{
    public static DbConnection Connection { get; private set; }

    public static DbContextOptionsBuilder<CoreDbContext> Create(DatabaseOptions dbOptions)
    {
        if (dbOptions.SqlLiteEnabled)
        {
            if (Connection == null)
            {
                throw new NullReferenceException(nameof(Connection));
            }

            return new DbContextOptionsBuilder<CoreDbContext>()
                .UseSqlite(Connection);
        }

        return new DbContextOptionsBuilder<CoreDbContext>()
            .UseSqlServer(dbOptions.ConnectionString);
        
    }

    public static DbConnection OpenInMemorySqliteDatabaseConnection()
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        return Connection;
    }
}