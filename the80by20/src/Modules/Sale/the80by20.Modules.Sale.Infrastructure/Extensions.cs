using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Sale.App.Clients.Solution;
using the80by20.Modules.Sale.Infrastructure.Clients;

namespace the80by20.Modules.Sale.Infrastructure
{
    public static class Extensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddSingleton<ISolutionApiClient, SolutionApiClient>();
            

            return services;
        }
    }
}
