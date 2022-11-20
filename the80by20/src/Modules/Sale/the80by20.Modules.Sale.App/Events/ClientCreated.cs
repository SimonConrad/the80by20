using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Sale.App.Events;

public record ClientCreated(Guid ClientId) : IEvent;