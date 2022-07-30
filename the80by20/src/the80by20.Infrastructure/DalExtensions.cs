using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Administration.MasterData;
using the80by20.App.Administration.Security;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Infrastructure.Administration.MasterData;
using the80by20.Infrastructure.Administration.Security;
using the80by20.Infrastructure.Core.SolutionToProblem;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Infrastructure.DAL.Misc;

namespace the80by20.Infrastructure;

// todo internal-visible like in my-spot
public static class DalExtensions
{
    private const string OptionsDataBaseName = "dataBase";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
        var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);
        services.AddDbContext<CoreDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services
            .AddSolutionToProblem()
            .AddAdministration();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }

    public static IServiceCollection AddSolutionToProblem(this IServiceCollection services)
    {
        services.AddScoped<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddScoped<IProblemAggregateRepository, EfProblemAggregateRepository>();
        services.AddScoped<ISolutionToProblemReadModelUpdates, EfSolutionToProblemReadModelRepository>();
        services.AddScoped<ISolutionToProblemReadModelQueries, EfSolutionToProblemReadModelRepository>();
        return services;
    }

    public static IServiceCollection AddAdministration(this IServiceCollection services)
    {
        services.AddScoped<ICategoryCrudRepository, CategoryCrudRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}