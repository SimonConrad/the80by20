
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using the80by20.Modules.Masterdata.Infrastructure.EF;
using the80by20.Modules.Solution.Infrastructure.EF;
using the80by20.Modules.Users.Infrastructure.EF;
using the80by20.Shared.Infrastucture.Services;

namespace the80by20.Tests.Integration.InMemorySqlLite.Setup;

internal static class SqlLiteIneMemoryManager
{
    public static SolutionDbContext SolutionDbContext { get; private set; }
    public static MasterDataDbContext MasterDataDbContext { get; private set; }
    public static UsersDbContext UsersDbContext { get; private set; }

    public static void SetupIoCContainer(IServiceCollection services, SqliteConnection connection)
    {
        RemoveServices(services);

        //var testDatabase = new TestSqlLiteInMemoryDatabase(connection);

        services.AddDbContext<SolutionDbContext>(x => x.UseSqlite(connection));
        services.AddDbContext<MasterDataDbContext>(x => x.UseSqlite(connection));
        services.AddDbContext<UsersDbContext>(x => x.UseSqlite(connection));
    }

    public static void RecreateDbs(SqliteConnection connection)
    {
        SolutionDbContext = new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>().UseSqlite(connection).Options);
        MasterDataDbContext = new MasterDataDbContext(new DbContextOptionsBuilder<MasterDataDbContext>().UseSqlite(connection).Options);
        UsersDbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().UseSqlite(connection).Options);

        UsersDbContext.Database.EnsureDeleted();
        UsersDbContext.Database.EnsureCreated();

        MasterDataDbContext.Database.EnsureDeleted();
        MasterDataDbContext.Database.EnsureCreated();

        SolutionDbContext.Database.EnsureDeleted();
        SolutionDbContext.Database.EnsureCreated();
    }

    public static async Task ApplyPendingMigrations()
    {
        //if (UsersDbContext.Database.GetPendingMigrations().Any())
        //{
        //    await UsersDbContext.Database.MigrateAsync();
        //}
        //if (MasterDataDbContext.Database.GetPendingMigrations().Any())
        //{
        //    await MasterDataDbContext.Database.MigrateAsync();
        //}
        //if (SolutionDbContext.Database.GetPendingMigrations().Any())
        //{
        //    await SolutionDbContext.Database.MigrateAsync();
        //}
    }

    private static void RemoveServices(IServiceCollection services)
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
                //typeof(IHostedService) && d.ImplementationType == typeof(DatabaseInitializer));
                typeof(IHostedService) && d.ImplementationType == typeof(AppInitializer));
        if (dbInitializerDescriptor != null)
        {
            services.Remove(dbInitializerDescriptor);
        }
    }
}