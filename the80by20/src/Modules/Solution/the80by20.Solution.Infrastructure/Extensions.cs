using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using the80by20.Solution.App.ReadModel;
using the80by20.Solution.Domain.Operations.Problem;
using the80by20.Solution.Domain.Operations.Solution;
using the80by20.Solution.Infrastructure.Behaviors;
using the80by20.Solution.Infrastructure.EF.Repositories;
using the80by20.Solution.Infrastructure.EF;
using the80by20.Solution.App.Commands.Problem.Handlers;
using the80by20.Shared.Infrastucture.SqlServer;


namespace the80by20.Solution.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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