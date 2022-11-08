using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.Domain.Shared.DomainServices;

namespace the80by20.Modules.Solution.App
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}