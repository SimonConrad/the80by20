using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Masterdata.App.Policies;
using the80by20.Masterdata.App.Services;

[assembly: InternalsVisibleTo("the80by20.Masterdata.Api")]
namespace the80by20.Masterdata.App
{
    internal static class Extensions
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<ICategoryDeletionPolicy, CategoryDeletionPolicy>();

            return services;
        }
    }
}
