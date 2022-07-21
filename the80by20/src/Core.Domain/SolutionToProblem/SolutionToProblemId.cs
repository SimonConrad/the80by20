using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionToProblemId
{
    public Guid Id { get;  init; }

    public static SolutionToProblemId FromGuid(Guid id) => new() { Id = id };
    public static SolutionToProblemId New() => new() { Id = Guid.NewGuid() };
}