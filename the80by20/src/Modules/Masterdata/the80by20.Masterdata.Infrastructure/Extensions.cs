using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Masterdata.App.CategoryCrud.Ports;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Masterdata.Infrastructure.EF.Repositories;
using the80by20.Shared.Abstractions.Dal;
using the80by20.Shared.Infrastucture;
using the80by20.Shared.Infrastucture.Configuration;

namespace the80by20.Masterdata.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddMasterdataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbCtxt(services, configuration);

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryCrudRepository, CategoryCrudRepository>();

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
