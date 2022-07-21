using Common.TacticalDDD;
using Core.Domain.SharedKernel;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionElement // TODO IEquatble to make SolutionElements._elements work
{
    public SolutionElementType Type { get; init; }
    public  string Link { get; init; }

    public static SolutionElement From(SolutionElementType type, string link) =>
        new() { Type = type, Link = link };


    // GetHashSet => tostinr propkow
}