using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Solution.Domain.Security.UserEntity;
using the80by20.Solution.Infrastructure.Security.Adapters.Users;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Security
{
    internal static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddSingleton<IPasswordManager, PasswordManager>();

            return services;
        }
    }
}