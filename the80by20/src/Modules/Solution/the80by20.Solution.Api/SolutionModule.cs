using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.App;
using the80by20.Modules.Solution.App.Solution.DTO;
using the80by20.Modules.Solution.App.Solution.Queries;
using the80by20.Modules.Solution.Domain;
using the80by20.Modules.Solution.Infrastructure;
using the80by20.Shared.Abstractions.Modules;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Shared.Infrastucture.Modules;

namespace the80by20.Modules.Solution.Api
{
    // TODO apply base controller and other module mechanisms from masterdatamodule
    internal class SolutionModule : IModule
    {
        public const string BasePath = "solution-to-problem";

        public string Name => "Solution";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            "solution"
        };

        public void Register(IServiceCollection services)
        {
            // INFO if needed service can be obtain by such code:
            // using var scope = serviceProvider.CreateScope();
            // scope.ServiceProvider.GetService...
            // so we don't need to create a constructor with passed to it dependencies

            services
                .AddDomain()
                .AddApplication()
                .AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            // INFO if needed service can be obtain by such code:
            // app.ApplicationServices.GetService...
            // so we don't need to create a constructor with passed to it dependencies

            app.UseModuleRequests()
                .Subscribe<GetSolutionToProblem, SolutionToProblemDto>("solutions/get",
                (query, sp) => sp.GetRequiredService<IQueryDispatcher>().QueryAsync(query));
        }
    }
}
