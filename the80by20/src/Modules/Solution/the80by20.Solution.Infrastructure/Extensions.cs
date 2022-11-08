using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using the80by20.Shared.Infrastucture.SqlServer;
using the80by20.Modules.Solution.Infrastructure.EF.Repositories;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.Infrastructure.Behaviors;
using the80by20.Modules.Solution.App.Commands.Problem.Handlers;
using the80by20.Modules.Solution.Infrastructure.EF;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Solution;

namespace the80by20.Modules.Solution.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSqlServer<SolutionDbContext>();

            AddSolutionToProblemDal(services);

            AddMediatRStaff(services);

            return services;
        }

        private static void AddMediatRStaff(IServiceCollection services)
        {
            // todo w zwi¹zku z tym, ¿e mediator mocno pl¹cze koncpet query i command, (nie sa on oddzielone)
            // to lepiej chyba przpi¹c siê na rozwi¹zanie od devmentors np te w myspot
            // albo rozdzielic w ramach mediar jako: https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs/

            services.AddMediatR(typeof(SolutionToProblemReadModelEventHandler));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(CreateProblemInputValidator).Assembly);
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