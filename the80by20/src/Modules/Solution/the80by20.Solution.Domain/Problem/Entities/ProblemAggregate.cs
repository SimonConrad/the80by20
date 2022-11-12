using the80by20.Modules.Solution.Domain.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Exceptions;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Problem.Entities;

[AggregateDdd]
public class ProblemAggregate : AggregateRoot // TODO make as sealed domain types
{
    public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();
    public bool Confirmed { get; private set; }
    public bool Rejected { get; private set; }

    protected ProblemAggregate()
    {
    }

    private ProblemAggregate(AggregateId id, RequiredSolutionTypes requiredSolutionTypes, int version = 0)
    {
        Id = id;
        RequiredSolutionTypes = requiredSolutionTypes;
        Version = version;
    }

    private ProblemAggregate(AggregateId id) => Id = id;

    public static ProblemAggregate New(Guid id, RequiredSolutionTypes requiredSolutionTypes)
    {
        var problem = new ProblemAggregate(id);
        problem.Update(requiredSolutionTypes);
        problem.ClearEvents();
        problem.Version = 0;
        return problem;
    }

    public void Update(RequiredSolutionTypes requiredSolutionTypes)
    {
        if (Confirmed)
            throw new ProblemException("Cannot update confirmed problem", Id.Value); // TODO create dedicated exceptions

        RequiredSolutionTypes = requiredSolutionTypes;
        IncrementVersion();
    }

    public void Confirm()
    {
        if (!RequiredSolutionTypes.Elements.Any())
            throw new ProblemException($"{nameof(Confirm)} Cannot confirm", Id.Value); // TODO create dedicated exceptions

        Confirmed = true;
        Rejected = false;

        AddEvent(new ProblemConfirmed(this)); // INFO IncrementVersion(); done in AddEvent
    }

    public void Reject()
    {
        Rejected = true;
        Confirmed = false;
        IncrementVersion();
    }
}