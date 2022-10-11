using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.AppLayer;

namespace the80by20.Infrastructure.Logging;

internal static class Extensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}