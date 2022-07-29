using Common.Dal;
using Core.App.Administration.MasterData;
using Core.App.Administration.Security;
using Core.App.Core.SolutionToProblem.ReadModel;
using Core.Domain.Core.SolutionToProblem.Operations;
using Core.Infrastructure.Administration.MasterData;
using Core.Infrastructure.Administration.Security;
using Core.Infrastructure.Core.SolutionToProblem;
using Core.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

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