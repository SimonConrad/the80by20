

using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace the80by20.Services.Sale.App.Events.External
{
    [Message("modular-monolith")]
    public record SolutionFinished(Guid solutionId) : IEvent;

}