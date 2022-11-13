using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Solution.Events
{
    public record SolutionFinished(SolutionToProblemAggregate solution) : IDomainEvent;
}
