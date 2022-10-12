using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.AppLayer;

namespace the80by20.Solution.Infrastructure.InputValidation
{
    internal static class Extensions
    {
        public static IServiceCollection AddInputValidation(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

            return services;
        }
    }
}