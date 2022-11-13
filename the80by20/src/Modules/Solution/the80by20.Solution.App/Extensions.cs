using Microsoft.Extensions.DependencyInjection;

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