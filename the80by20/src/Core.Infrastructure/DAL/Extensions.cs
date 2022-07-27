using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Repositories.SolutionToProblem;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL;

// todo internal-visible like in my-spot
public static class Extensions
{
    public const string OptionsSectionSqlServerName = "sqlServer";
    public const string OptionsSectionAppName = "app";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        //// TODO jeden db context, z róznymi politykami
        //if (configuration.GetOptions<AppOptions>(OptionsSectionAppName).SqlLiteEnabled)
        //{
        //    services.AddSingleton(_ => CoreSqLiteDbContext.CreateInMemoryDatabase());
        //    services.AddDbContext<CoreSqLiteDbContext>();
        //    services.AddTransient<DbContext>(ctx => ctx.GetRequiredService<CoreSqLiteDbContext>());
        //}


        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(OptionsSectionSqlServerName));
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(OptionsSectionSqlServerName);
        services.AddDbContext<CoreSqlServerDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));

        services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddTransient<ISolutionToProblemReadModelRepository, EfSolutionToProblemReadModelRepository>();

        return services;
    }

}
