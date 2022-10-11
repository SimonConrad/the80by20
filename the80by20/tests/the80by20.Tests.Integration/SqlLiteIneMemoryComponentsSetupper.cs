using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using the80by20.Infrastructure.DAL;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Tests.Integration;

internal static class SqlLiteIneMemoryComponentsSetupper
{
    internal static (SqliteConnection connection, IWithCoreDbContext ctxt) Setup(IServiceCollection services)
    {
        // for sqllite
        var descriptor1 = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<CoreDbContext>));

        if (descriptor1 != null)
        {
            services.Remove(descriptor1);
        }

        //DatabaseInitializer
        var descriptor2 = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(IHostedService) && d.ImplementationType == typeof(DatabaseInitializer));
        if (descriptor2 != null)
        {
            services.Remove(descriptor2);
        }

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var testDatabase = new TestSqlLiteInMemoryDatabase(connection);

        services.AddDbContext<CoreDbContext>(x => x.UseSqlite(connection));

        return (connection, testDatabase);
    }
}