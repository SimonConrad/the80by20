using the80by20.Modules.Solution.Domain.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Exceptions;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Problem.Entities;

// INFO
// Aggregate defines boundary of data consistancy, it encapsulates data that should be immedietly (strongly) consistent (not eventually consistent)
// scope of such data is discovered during event storming modelling phase


// Aggregate has its identity, in contrast to value object which is comapred by vaue each aggregate object is unique due to its identity - aggregates are compared by identity

// data (also named: information, attributes, properties) inside it (which is persisted) cannot be in inconsitent state
// - that's way we only allow to change them via methods (setters are private)

// in the methods (which are aggregate bahvaiors) invariants are applied
// invariants are buisness rules defining how to change grouped in aggregate data (package of data)
// so that this package of data as a whole is consitent, persisted in db in consitent way
// (e.x. of inconsitency of problem-aggregate object - it is simultaneously rejected and confirmed)

// incosistency problem is easily achieved if no encpasulation and beahvior is in entity and entity state is change by a service
// (service is setting properties values via setter), however such anemic entites + services approach is ok in crud like scenarios

// how to define aggregate boiundaries (aggregate boundary - scope of data and behaviors in aggregate) - event storming phases,
// then tehnique of including only this data which is used by invariants + some other data that fits naturally to aggregate
// rather one teble for aggregate - value object serilized to json in column
// small agregates

[AggregateDdd]
public sealed class ProblemAggregate : AggregateRoot // TODO make as sealed domain types
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

        problem.AddEvent(new ProblemRequested(problem));
        return problem;
    }

    public void Update(RequiredSolutionTypes requiredSolutionTypes)
    {
        if (Confirmed)
            throw new ProblemException("Cannot update confirmed problem", Id.Value); // TODO create dedicated exception

        RequiredSolutionTypes = requiredSolutionTypes;
    }

    public void Confirm()
    {
        if (!RequiredSolutionTypes.Elements.Any())
            throw new NoRequiredSolutionTypesException($"{nameof(Confirm)} Cannot confirm", Id.Value);

        Confirmed = true;
        Rejected = false;

        AddEvent(new ProblemConfirmed(this)); // INFO IncrementVersion(); is done in AddEvent method
    }

    public void Reject()
    {
        Rejected = true;
        Confirmed = false;

        AddEvent(new ProblemRejected(this));
    }
}