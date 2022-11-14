using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Infrastucture.SqlServer;
using the80by20.Modules.Solution.Infrastructure.EF.Repositories;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.Infrastructure.EF;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Solution.Repositories;

namespace the80by20.Modules.Solution.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSqlServer<SolutionDbContext>();

            AddSolutionToProblemDal(services);

            //AddMediatRStaff(services);

            return services;
        }

        private static void AddMediatRStaff(IServiceCollection services)
        {
            //services.AddMediatR(typeof(ProblemReadModelHandler));

            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            //services.AddValidatorsFromAssembly(typeof(CreateProblemInputValidator).Assembly);
            // info more problems with this then pozytku services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }

        private static void AddSolutionToProblemDal(IServiceCollection services)
        {
            services.AddScoped<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
            services.AddScoped<ISolutionToProblemReadModelUpdates, EfSolutionToProblemReadModelRepository>();
            services.AddScoped<ISolutionToProblemReadModelQueries, EfSolutionToProblemReadModelRepository>();

            services.AddScoped<IProblemAggregateRepository, EfProblemAggregateRepository>();
        }
    }
}