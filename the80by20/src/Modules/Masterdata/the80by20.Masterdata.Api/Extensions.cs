using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Masterdata.App;
using the80by20.Masterdata.Infrastructure;

[assembly: InternalsVisibleTo("the80by20.Bootstrapper")]
namespace the80by20.Masterdata.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddMasterdata(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApp();
            services.AddInfrastructure(configuration);
            return services;
        }
    }
}
