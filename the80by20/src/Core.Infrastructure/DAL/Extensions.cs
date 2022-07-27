using System.Data.Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Repositories.SolutionToProblem;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL;

// todo internal-visible like in my-spot
public static class Extensions
{
    public const string OptionsSectionSqlServerName = "sqlServer";
    public const string OptionsSectionAppName = "app";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetOptions<AppOptions>(OptionsSectionAppName).SqlLiteEnabled)
        {
            services.AddSingleton(_ => CreateInMemoryDatabase());
            services.AddDbContext<CoreDbContext>((x, y) =>
            {
                var connection = x.GetRequiredService<DbConnection>();
                y.UseSqlite(connection);
            });
        }
        else
        {
            services.Configure<SqlServerOptions>(configuration.GetRequiredSection(OptionsSectionSqlServerName));
            var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(OptionsSectionSqlServerName);
            services.AddDbContext<CoreDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));
        }

        services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddTransient<ISolutionToProblemReadModelRepository, EfSolutionToProblemReadModelRepository>();

        return services;
    }


    public static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        return connection;
    }

}
