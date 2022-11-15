namespace the80by20.Shared.Abstractions.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
