using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Problem.Events
{

    public record ProblemRejected(ProblemAggregate problem) : IDomainEvent;

}
