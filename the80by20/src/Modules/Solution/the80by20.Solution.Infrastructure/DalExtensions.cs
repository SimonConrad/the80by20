using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Abstractions.Dal;
using the80by20.Solution.App.Security.Ports;
using the80by20.Solution.App.SolutionToProblem.ReadModel;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Solution;
using the80by20.Solution.Infrastructure.DAL;
using the80by20.Solution.Infrastructure.EF;
using the80by20.Solution.Infrastructure.EF.Repositories;
using the80by20.Solution.Infrastructure.Security.Adapters.Users;

namespace the80by20.Solution.Infrastructure
{
    // todo internal-visible like in my-spot
    public static class DalExtensions
    {
        private const string OptionsDataBaseName = "dataBase";

        public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
            var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);
            services.AddDbContext<SolutionDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));

            services
                .AddSecurityDal()
                .AddSolutionToProblemDal();


            // info only used in commands done like the80by20.App.Abstractions.ICommand
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

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
}