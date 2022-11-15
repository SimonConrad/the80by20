namespace the80by20.Shared.Abstractions.Events
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent // INFO in, as this type os for method input, it is contravariant
    {
        Task HandleAsync(TEvent @event); 
    }
}
