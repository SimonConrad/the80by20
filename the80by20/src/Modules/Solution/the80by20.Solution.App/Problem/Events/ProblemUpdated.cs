using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Problem.Events
{
    public record ProblemUpdated(Guid problemId, ProblemAggregate problemAggregate, ProblemCrudData problemCrudData) : IEvent;
}
