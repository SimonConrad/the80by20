
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using the80by20.Bootstrapper;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Solution.Infrastructure.EF;
using the80by20.Users.Infrastructure.EF;

namespace the80by20.Tests.Integration;

internal static class SqlLiteIneMemoryComponentsSetupper
{
    internal static (SqliteConnection connection, IWithCoreDbContext ctxt) Setup(IServiceCollection services)
    {
        var solutionDbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<SolutionDbContext>));

        if (solutionDbContextDescriptor != null)
        {
            services.Remove(solutionDbContextDescriptor);
        }

        var masterDataDbCtxtDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<MasterDataDbContext>));

        if (masterDataDbCtxtDescriptor != null)
        {
            services.Remove(masterDataDbCtxtDescriptor);
        }

        var usersDbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<UsersDbContext>));

        if (usersDbContextDescriptor != null)
        {
            services.Remove(usersDbContextDescriptor);
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

        services.AddDbContext<SolutionDbContext>(x => x.UseSqlite(connection));
        services.AddDbContext<MasterDataDbContext>(x => x.UseSqlite(connection));
        services.AddDbContext<UsersDbContext>(x => x.UseSqlite(connection));

        return (connection, testDatabase);
    }
}