using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Masterdata.App.Repositories;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Masterdata.Infrastructure.EF.Repositories;
using the80by20.Shared.Infrastucture;
using the80by20.Shared.Infrastucture.Configuration;

[assembly: InternalsVisibleTo("the80by20.Masterdata.Api")]
namespace the80by20.Masterdata.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbCtxt(services, configuration);
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

            return services;
        }

        private static void AddDbCtxt(IServiceCollection services, IConfiguration configuration)
        {
            const string OptionsDataBaseName = "dataBase";
            services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
            var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);
            services.AddDbContext<MasterDataDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));
        }
    }
}
