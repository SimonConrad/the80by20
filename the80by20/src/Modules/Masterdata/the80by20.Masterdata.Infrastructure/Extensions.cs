using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Masterdata.App.Repositories;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Masterdata.Infrastructure.EF.Repositories;
using the80by20.Shared.Abstractions;
using the80by20.Shared.Infrastucture.SqlServer;

[assembly: InternalsVisibleTo("the80by20.Masterdata.Api")]
namespace the80by20.Masterdata.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<MasterDataDbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDataSeeder, MasterDataSeeder>();

            //services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

            return services;
        }
    }
}
