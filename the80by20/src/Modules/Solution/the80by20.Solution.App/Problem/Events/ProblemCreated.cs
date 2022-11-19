using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Problem.Events
{
    public record ProblemCreated(ProblemAggregate problemAggregate, ProblemCrudData problemCrudData) : IEvent;

}
