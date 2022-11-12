namespace the80by20.Shared.Abstractions.Kernel;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(params IDomainEvent[] events);
}
