using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Shared.Infrastucture.Modules
{
    internal sealed class ModuleSubscriber : IModuleSubscriber
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IServiceProvider _serviceProvider;

        public ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
        {
            _moduleRegistry = moduleRegistry;
            _serviceProvider = serviceProvider;
        }

        public IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
            Func<TRequest, IServiceProvider, Task<TResponse>> action)
            where TRequest : class where TResponse : class
        {
            _moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
                async request =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    return await action((TRequest)request, scope.ServiceProvider);
                });

            return this;
        }
    }
}
