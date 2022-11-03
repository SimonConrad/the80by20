using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Masterdata.App.Repositories;
using the80by20.Modules.Masterdata.Infrastructure.EF;
using the80by20.Modules.Masterdata.Infrastructure.EF.Repositories;
using the80by20.Shared.Infrastucture.Services;
using the80by20.Shared.Infrastucture.SqlServer;

namespace the80by20.Modules.Masterdata.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSqlServer<MasterDataDbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDataSeeder, MasterDataSeeder>();

            //services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

            return services;
        }
    }
}
