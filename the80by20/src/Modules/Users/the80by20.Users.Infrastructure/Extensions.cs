using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using the80by20.Users.App.Commands.Handlers;
using the80by20.Users.App.Ports;
using the80by20.Users.Domain.UserEntity;
using the80by20.Users.Infrastructure.Auth;
using the80by20.Users.Infrastructure.EF.Repositories;
using the80by20.Users.Infrastructure.Security;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using the80by20.Users.Infrastructure.EF;
using the80by20.Shared.Infrastucture.Decorators;
using the80by20.Shared.Infrastucture.EF;
using System.Runtime.CompilerServices;
using the80by20.Shared.Abstractions.Dal;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Infrastucture.SqlServer;

[assembly: InternalsVisibleTo("the80by20.Users.Api")]
namespace the80by20.Users.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<SignUpInputValidator>();

            AddSecurity(services);

            AddAuth(services, configuration);

            AddCommandHandlersDecorators(services);

            AddQueryHandlers(services);

            services.AddSqlServer<UsersDbContext>();

            return services;
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            // INFO CQRS queryhandlers registration
            var infrastructureAssembly = typeof(Extensions).Assembly;
            services.Scan(s => s.FromAssemblies(infrastructureAssembly)
               .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddSingleton<IPasswordManager, PasswordManager>();
        }

        private static void AddCommandHandlersDecorators(IServiceCollection services)
        {
            // info only used in commands done like the80by20.App.Abstractions.ICommand
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        }

        private static void AddAuth(IServiceCollection services, IConfiguration configuration)
        {
            const string OptionsSectionName = "auth";
            var options = new AuthOptions();
            configuration.GetSection(OptionsSectionName).Bind(options);

            services
                .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
                .AddSingleton<IAuthenticator, Authenticator>()
                .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Audience = options.Audience;
                    o.IncludeErrorDetails = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = options.Issuer,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                    };
                });

            // TODO do more asp.net identity api with requiremnt based on udemy asp.net identity security; chapsas; .net docs
            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("is-admin", policy =>
                {
                    policy.RequireRole("admin");
                });
            });
        }
    }
}
