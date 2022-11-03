using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Operations.Problem;

[ValueObjectDdd]
public sealed record ProblemId
{
    public Guid Value { get; }

    public ProblemId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static ProblemId New() => new(Guid.NewGuid());

    public static implicit operator Guid(ProblemId id) => id.Value;

    public static implicit operator ProblemId(Guid value) => new(value);

    public override string ToString() => Value.ToString("N");
}