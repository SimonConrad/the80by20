using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Problem.Events
{
    // INFO
    // Can be handled by problem-confirmed-handler in application-layer
    // create solution-to-problem aggregate in handling code

    // TODO do above
    public record ProblemConfirmed(ProblemAggregate problem) : IDomainEvent;

}
