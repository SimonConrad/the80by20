using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

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

    public void ConfirmProblem()
    {
        if (!RequiredSolutionTypes.Elements.Any())
            throw new DomainException("Cannot confirm");
        
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