using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Sale.Infrastructure;
using the80by20.Shared.Abstractions.Modules;
using the80by20.Shared.Infrastucture.Modules;

namespace the80by20.Modules.Masterdata.Api
{
    internal class SaleModule : IModule
    {
        public const string BasePath = "sale";

        public string Name => "Sale";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            "sale"
        };

        public void Register(IServiceCollection services)
        {
            // INFO if needed service can be obtain by such code:
            // using var scope = serviceProvider.CreateScope();
            // scope.ServiceProvider.GetService...
            // so we don't need to create a constructor with passed to it dependencies

            services
               .AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            // INFO if needed service can be obtain by such code:
            // app.ApplicationServices.GetService...
            // so we don't need to create a constructor with passed to it dependencies
        }
    }
}
