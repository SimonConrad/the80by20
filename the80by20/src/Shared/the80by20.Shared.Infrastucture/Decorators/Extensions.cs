using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Shared.Infrastucture.Decorators
{
    public static class Extensions
    {
        public static IServiceCollection AddCommandHandlersDecorators(this IServiceCollection services)
        {
            // info only used in commands done like the80by20.App.Abstractions.ICommand
            //services.AddScoped<IUnitOfWork2, EfUnitOfWork>();
            //services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            return services;
        }
    }
}
