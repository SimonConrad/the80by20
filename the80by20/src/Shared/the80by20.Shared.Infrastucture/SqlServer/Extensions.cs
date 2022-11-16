using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Infrastucture.SqlServer.Decorators;

namespace the80by20.Shared.Infrastucture.SqlServer
{
    public static class Extensions
    {
        internal static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            var options = services.GetOptions<DatabaseOptions>("dataBase");
            services.AddSingleton(options);
            services.AddSingleton(new UnitOfWorkTypeRegistry());

            return services;
        }

        public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));

            return services;
        }

        public static IServiceCollection AddSqlServer<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<DatabaseOptions>("dataBase");
            services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));

            return services;

        }

        public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
            where TUnitOfWork : class, IUnitOfWork where TImplementation : class, TUnitOfWork
        {
            services.AddScoped<TUnitOfWork, TImplementation>();
            services.AddScoped<IUnitOfWork, TImplementation>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();

            return services;
        }
    }
}
