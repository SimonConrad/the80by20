using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Shared.Infrastucture.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            var tasks = handlers.Select(x => x.HandleAsync(@event));
            await Task.WhenAll(tasks);
        }

        // INFO not synchronous - fire and forget way
        public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            var tasks = handlers.Select(x => x.HandleAsync(@event));
            Task.WhenAll(tasks);
        }
    }
}
