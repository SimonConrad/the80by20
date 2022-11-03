using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.Domain;
using the80by20.Modules.Solution.Infrastructure;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Modules.Solution.Api
{
    // TODO apply base controller and other module mechanisms from masterdatamodule
    internal class SolutionModule : IModule
    {
        public const string BasePath = "solution";

        public string Name => "Solution";

        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            // INFO if needed service can be obtain by such code:
            // using var scope = serviceProvider.CreateScope();
            // scope.ServiceProvider.GetService...
            // so we don't need to create a constructor with passed to it dependencies


            services.AddDomain();
            services.AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            // INFO if needed service can be obtain by such code:
            // app.ApplicationServices.GetService...
            // so we don't need to create a constructor with passed to it dependencies
        }
    }
}
