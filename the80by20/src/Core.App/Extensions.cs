using Core.App.SolutionToProblem.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Core.App
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

            services.AddTransient<CreateProblemCommandHandler>();
        
            return services;
        }
    }
}
