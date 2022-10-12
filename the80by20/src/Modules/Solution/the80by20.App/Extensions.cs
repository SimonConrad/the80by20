using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.AppLayer;

namespace the80by20.App
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(Extensions).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}

// todo udekorowac handlera logownica i transakcja https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Application/ConfigureServices.cs
// todo wlaidacja wejscia fluent validator
// todo otwarte taby w my-spot
