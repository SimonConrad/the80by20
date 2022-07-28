using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Repositories.SolutionToProblem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.DAL;

// todo internal-visible like in my-spot
public static class Extensions
{
    private const string OptionsDataBaseName = "dataBase";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
        var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);

        if (dataBaseOptions.SqlLiteEnabled)
        {
            CoreDbContextFactory.OpenInMemorySqliteDatabaseConnection();
            services.AddDbContext<CoreDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                dbContextOptionsBuilder.UseSqlite(CoreDbContextFactory.Connection);
            });
        }
        else
        {
            services.AddDbContext<CoreDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));
        }

        services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddTransient<ISolutionToProblemReadModelRepository, EfSolutionToProblemReadModelRepository>();

        return services;
    }
}
