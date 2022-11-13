using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Solution.App.Solution.Services
{
    public interface IEventMapper
    {
        IMessage Map(IDomainEvent @event);
        IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events);
    }
}
