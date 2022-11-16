using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using the80by20.Shared.Infrastucture.Decorators;
using the80by20.Shared.Abstractions.Dal;
using the80by20.Shared.Abstractions.Commands;
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

            AddCommandHandlersDecorators(services);

            services.AddSqlServer<UsersDbContext>();

            return services;
        }

        private static void AddCommandHandlersDecorators(IServiceCollection services)
        {
            // info only used in commands done like the80by20.App.Abstractions.ICommand
            //services.AddScoped<IUnitOfWork2, EfUnitOfWork>();
            //services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            //services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

            //services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        }
    }
}
