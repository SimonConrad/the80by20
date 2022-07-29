using Microsoft.Extensions.DependencyInjection;

namespace the80by20.App
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            //services.Scan(s => s.FromAssemblies(applicationAssembly)
            //    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            return services;
        }
    }
}
