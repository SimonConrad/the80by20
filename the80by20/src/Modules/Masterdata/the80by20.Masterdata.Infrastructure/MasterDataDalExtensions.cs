using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Masterdata.App.CategoryCrud.Ports;
using the80by20.Masterdata.Infrastructure.EF;
using the80by20.Masterdata.Infrastructure.EF.Repositories;
using the80by20.Shared.Abstractions.Dal;

namespace the80by20.Masterdata.Infrastructure
{
    public static class MasterDataDalExtensions
    {
        private const string OptionsDataBaseName = "dataBase";

        // TODO zawołac w boostraperze
        public static IServiceCollection AddMasterDataDal(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
            var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);

            services.AddDbContext<MasterDataDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));

            services.AddMasterDataDal();

            services.AddHostedService<DatabaseInitializer>();


            return services;
        }

        private static IServiceCollection AddMasterDataDal(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryCrudRepository, CategoryCrudRepository>();

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
