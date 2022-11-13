using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.App.Solution.Services;

namespace the80by20.Modules.Solution.App
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddSingleton<IEventMapper, EventMapper>();
    }
}