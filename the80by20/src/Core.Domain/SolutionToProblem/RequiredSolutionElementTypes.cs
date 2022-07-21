using Common.TacticalDDD;
using Core.Domain.SharedKernel;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class RequiredSolutionElementTypes // todo immutable collection like SolutionElements
{

    public IEnumerable<SolutionElementType> Elements { get; init; }


    public static RequiredSolutionElementTypes FromSolutionElements(params SolutionElementType[] elements) => new()
    {
        Elements = elements.Distinct()
    };
}