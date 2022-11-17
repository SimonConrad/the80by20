using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using the80by20.Shared.Infrastucture.SqlServer;
using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Infrastructure.EF;
using the80by20.Modules.Users.Infrastructure.Security;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Modules.Users.App.Commands.Handlers;
using the80by20.Modules.Users.Infrastructure.EF.Repositories;

namespace the80by20.Modules.Users.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SignUpInputValidator>();

            services.AddScoped<IUserRepository, UserRepository>();

            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddSingleton<IPasswordManager, PasswordManager>();


            services.AddSqlServer<UsersDbContext>();

            return services;
        }
    }
}
