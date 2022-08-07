using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Abstractions;

namespace the80by20.Infrastructure.InputValidation;

internal static class Extensions
{
    public static IServiceCollection AddInputValidation(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

        return services;
    }
}