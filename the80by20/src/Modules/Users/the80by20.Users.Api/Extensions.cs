using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Users.App;
using the80by20.Users.Infrastructure;

namespace the80by20.Users.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApp();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}