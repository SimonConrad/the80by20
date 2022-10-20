using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Solution.Domain;
using the80by20.Solution.Infrastructure;

[assembly: InternalsVisibleTo("the80by20.Bootstrapper")]
namespace the80by20.Solution.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddSolution(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomain();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
