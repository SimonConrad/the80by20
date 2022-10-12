using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using the80by20.Masterdata.Infrastructure.EF;

namespace the80by20.Tests.Integration;

internal static class SqlLiteIneMemoryComponentsSetupper
{
    internal static (SqliteConnection connection, IWithCoreDbContext ctxt) Setup(IServiceCollection services)
    {
        var coreDbCtxtDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<CoreDbContext>));

        if (coreDbCtxtDescriptor != null)
        {
            services.Remove(coreDbCtxtDescriptor);
        }

        var masterDataDbCtxtDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<MasterDataDbContext>));

        if (masterDataDbCtxtDescriptor != null)
        {
            services.Remove(masterDataDbCtxtDescriptor);
        }

        //DatabaseInitializer
        var dbInitializerDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(IHostedService) && d.ImplementationType == typeof(DatabaseInitializer));
        if (dbInitializerDescriptor != null)
        {
            services.Remove(dbInitializerDescriptor);
        }

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var testDatabase = new TestSqlLiteInMemoryDatabase(connection);

        services.AddDbContext<CoreDbContext>(x => x.UseSqlite(connection));
        services.AddDbContext<MasterDataDbContext>(x => x.UseSqlite(connection));

        return (connection, testDatabase);
    }
}