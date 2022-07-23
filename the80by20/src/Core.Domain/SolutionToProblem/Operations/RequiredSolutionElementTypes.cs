using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;

namespace Core.Domain.SolutionToProblem.Operations;

[ValueObjectDdd]
public class RequiredSolutionElementTypes // todo immutable collection like SolutionElements
{
    public IEnumerable<SolutionElementType> Elements { get; init; }

    public static RequiredSolutionElementTypes From(params SolutionElementType[] elements) => new()
    {
        Elements = elements.Distinct()
    };
}