using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

[AggregateDdd]
public class ProblemAggregate : Versionable, IEquatable<ProblemAggregate>
{
    protected ProblemAggregate()
    {
    }

    public ProblemId Id { get; private set; }
    public RequiredSolutionElementTypes RequiredSolutionElementTypes { get; private set; } = RequiredSolutionElementTypes.Empty();
    public bool Confirmed { get; private set; }
    public bool Rejected { get; private set; }


    private ProblemAggregate(ProblemId id, 
        RequiredSolutionElementTypes requiredSolutionElementTypes)
    {
        Id = id;
        RequiredSolutionElementTypes = requiredSolutionElementTypes;
    }

    public static ProblemAggregate New(
        RequiredSolutionElementTypes requiredSolutionElementTypes,
        string description,
        Guid category) => new ProblemAggregate(ProblemId.New(), 
        requiredSolutionElementTypes);

    public void Update(RequiredSolutionElementTypes requiredSolutionElementTypes,
        string description,
        Guid category)
    {
        if (Confirmed)
            throw new DomainException("Cannot update confirmed problem");

        RequiredSolutionElementTypes = requiredSolutionElementTypes;
    }

    public void ConfirmProblem()
    {
        Confirmed = true;
        Rejected = false;
    }
    public void RejectProblem()
    {
        Rejected = true;
        Confirmed = false;
    }

    public bool Equals(ProblemAggregate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProblemAggregate)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}