using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Modules.Solution.App.Solution.Services;

[assembly: InternalsVisibleTo("the80by20.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyAssemblyGen2")]
namespace the80by20.Modules.Solution.App
{
    
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddSingleton<IEventMapper, EventMapper>();
    }
}