using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Shared.Infrastucture.Kernel
{
    internal sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync(params IDomainEvent[] events)
        {
            if (events is null || !events.Any())
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                var tasks = handlers.Select(x => (Task)handlerType
                    .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, new[] { @event }));

                await Task.WhenAll(tasks);
            }
        }
    }
}
