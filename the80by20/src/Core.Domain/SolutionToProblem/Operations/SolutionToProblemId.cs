using Common.DDD;

namespace Core.Domain.SolutionToProblem.Operations;

[ValueObjectDdd]
// INFO record gives out of the box, following value objects characteristics:
// - checking equality by value (by guid Value in this case); don't need to implement IEquatable<SolutionToProblemId>
// - immutaiblity guarding (cannot do: var id = new(Guid.NewGuid()); id.Value = guid.NewGuid())
public sealed record SolutionToProblemId 
{
    public Guid Value { get; }

    public SolutionToProblemId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static SolutionToProblemId New() => new(Guid.NewGuid());

    public static implicit operator Guid(SolutionToProblemId id) => id.Value;

    public static implicit operator SolutionToProblemId(Guid value) => new(value);

    public override string ToString() => Value.ToString("N");  
}