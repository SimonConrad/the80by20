using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Masterdata.App.Policies;
using the80by20.Modules.Masterdata.App.Services;

namespace the80by20.Modules.Masterdata.App
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
