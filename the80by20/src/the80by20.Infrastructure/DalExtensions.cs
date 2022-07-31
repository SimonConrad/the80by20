using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.App.MasterData;
using the80by20.App.MasterData.CategoryCrud;
using the80by20.App.MasterData.CategoryCrud.Ports;
using the80by20.App.Security.Ports;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Infrastructure.Core.SolutionToProblem;
using the80by20.Infrastructure.Core.SolutionToProblem.Adapters;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Infrastructure.DAL.Misc;
using the80by20.Infrastructure.MasterData.Adapters;
using the80by20.Infrastructure.Security;
using the80by20.Infrastructure.Security.Adapters.Users;

namespace the80by20.Infrastructure;

// todo internal-visible like in my-spot
public static class DalExtensions
{
    private const string OptionsDataBaseName = "dataBase";

    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
        var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);
        services.AddDbContext<CoreDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));

        services
            .AddMasterDataDal()
            .AddSecurityDal()
            .AddSolutionToProblemDal();
            
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }

    private static IServiceCollection AddMasterDataDal(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICategoryCrudRepository, CategoryCrudRepository>();

        return services;
    }

    private static IServiceCollection AddSecurityDal(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddSolutionToProblemDal(this IServiceCollection services)
    {
        services.AddScoped<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddScoped<ISolutionToProblemReadModelUpdates, EfSolutionToProblemReadModelRepository>();
        services.AddScoped<ISolutionToProblemReadModelQueries, EfSolutionToProblemReadModelRepository>();
        
        services.AddScoped<IProblemAggregateRepository, EfProblemAggregateRepository>();

        return services;
    }
}