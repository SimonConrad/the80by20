using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Commands;


namespace the80by20.Modules.Users.App
{
    internal static class Extensions
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            AddCommandHandlers(services);

            return services;
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            // INFO CQRS commandhandlers registration
            var applicationAssembly = typeof(Extensions).Assembly;
            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}

// todo udekorowac handlera logownica i transakcja https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Application/ConfigureServices.cs
// todo wlaidacja wejscia fluent validator
// todo otwarte taby w my-spot

