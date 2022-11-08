using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Problem;

[AggregateDdd]
public class ProblemAggregate : Versionable, IEquatable<ProblemAggregate>
{
    protected ProblemAggregate()
    {
    }

    public ProblemId Id { get; private set; }
    public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();
    public bool Confirmed { get; private set; }
    public bool Rejected { get; private set; }


    private ProblemAggregate(ProblemId id, RequiredSolutionTypes requiredSolutionTypes)
    {
        Id = id;
        RequiredSolutionTypes = requiredSolutionTypes;
    }

    public static ProblemAggregate New(RequiredSolutionTypes requiredSolutionTypes)
        => new ProblemAggregate(ProblemId.New(), requiredSolutionTypes);

    public void Update(RequiredSolutionTypes requiredSolutionTypes)
    {
        if (Confirmed)
            throw new DomainException("Cannot update confirmed problem");

        RequiredSolutionTypes = requiredSolutionTypes;
    }

    public void Confirm()
    {
        if (!RequiredSolutionTypes.Elements.Any())
            throw new DomainException($"{nameof(Confirm)} Cannot confirm");

        Confirmed = true;
        Rejected = false;
    }

    public void Reject()
    {
        Rejected = true;
        Confirmed = false;
    }

    public bool Equals(ProblemAggregate other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ProblemAggregate)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}