namespace the80by20.Shared.Abstractions.Kernel;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event);
}