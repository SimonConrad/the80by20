using the80by20.Shared.Abstractions.Events;

namespace the80by20.Saga.Messages;

public record SolutionArchived(Guid SolutionId, Guid UserId) : IEvent;