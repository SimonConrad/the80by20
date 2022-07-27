using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Repositories.SolutionToProblem;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL;

// todo internal-visible like in my-spot
public static class Extensions
{
    private const string OptionsSectionName = "SqlServer";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton(_ => CoreSqLiteDbContext.CreateInMemoryDatabase());
        //services.AddDbContext<CoreSqLiteDbContext>();
        //services.AddTransient<DbContext>(ctx => ctx.GetRequiredService<CoreSqLiteDbContext>());

        // todo enable based on appsetting from configuration
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(OptionsSectionName);
        services.AddDbContext<CoreSqlServerDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));


        //services.AddTransient<CreateProblemCommandHandler>();
        services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddTransient<ISolutionToProblemReadModelRepository, EfSolutionToProblemReadModelRepository>();

        return services;
    }

}
