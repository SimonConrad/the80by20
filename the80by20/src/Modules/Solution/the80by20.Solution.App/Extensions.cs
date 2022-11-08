using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.Domain.Operations.DomainServices;

namespace the80by20.Modules.Solution.App
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}