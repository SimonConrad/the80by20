using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Infrastucture.Decorators;
using the80by20.Shared.Infrastucture.EF;

namespace the80by20.Users.App
{
    public static class Extensions
    {
        public static IServiceCollection AddUsersApp(this IServiceCollection services)
        {
            // INFO CQRS commandhandlers registration
            var applicationAssembly = typeof(Extensions).Assembly;
            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            AddCommandHAndlersDecorators(services);

            return services;
        }

        private static void AddCommandHAndlersDecorators(IServiceCollection services)
        {
            // info only used in commands done like the80by20.App.Abstractions.ICommand
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        }
    }
}

// todo udekorowac handlera logownica i transakcja https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Application/ConfigureServices.cs
// todo wlaidacja wejscia fluent validator
// todo otwarte taby w my-spot

