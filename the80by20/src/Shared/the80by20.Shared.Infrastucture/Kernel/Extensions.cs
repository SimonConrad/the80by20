using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Shared.Infrastucture.Kernel
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}
