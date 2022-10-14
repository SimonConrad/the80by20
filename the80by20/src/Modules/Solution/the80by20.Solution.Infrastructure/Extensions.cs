using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using the80by20.Shared.Infrastucture;
using the80by20.Shared.Infrastucture.Configuration;
using the80by20.Solution.App.ReadModel;
using the80by20.Solution.Domain.Operations.Problem;
using the80by20.Solution.Domain.Operations.Solution;
using the80by20.Solution.Infrastructure.Behaviors;
using the80by20.Solution.Infrastructure.EF.Repositories;
using the80by20.Solution.Infrastructure.EF;

using the80by20.Solution.App.CommandsHandlers.ProblemHandlers;

namespace the80by20.Solution.Infrastructure
{
    public static class Extensions
    {
        public const string OptionsSectionAppName = "app";
        private const string OptionsDataBaseName = "dataBase";

        public static IServiceCollection AddSolutionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
           
            AddDbCtxt(services, configuration);
            AddSolutionToProblemDal(services);

            AddMediatRStaff(services);

            return services;
        }

        private static void AddMediatRStaff(IServiceCollection services)
        {
            // todo w zwi¹zku z tym, ¿e mediator mocno pl¹cze koncpet query i command, nie sa on oddzielone lepiej chyba przpi¹c siê na rozwi¹zanie od devmentors np te w mysport
            // albo rozdzielic w ramach mediar jako: https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs/
            services.AddMediatR(typeof(SolutionToProblemReadModelEventHandler));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(CreateProblemInputValidator).Assembly);
            // info more problems with this then pozytku services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }

        

        private static void AddDbCtxt(IServiceCollection services, IConfiguration configuration)
        {
            const string OptionsDataBaseName = "dataBase";
            services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
            var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);
            services.AddDbContext<SolutionDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));
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