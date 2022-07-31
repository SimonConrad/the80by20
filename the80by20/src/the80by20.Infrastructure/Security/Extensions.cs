using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Administration.Security;
using the80by20.App.Administration.Security.User;

namespace the80by20.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}