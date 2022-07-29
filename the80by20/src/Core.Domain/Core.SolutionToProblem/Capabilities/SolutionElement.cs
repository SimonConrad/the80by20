using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;

namespace Core.Domain.Core.SolutionToProblem.Capabilities;

[ValueObjectDdd]
public sealed record SolutionElement
{
    public SolutionElementType Type { get; }
    public  string Link { get; }

    private SolutionElement(SolutionElementType type, string link)
    {
        Type = type;
        Link = link;
    }

    public static SolutionElement From(SolutionElementType type, string link) => new(type, link);

    public override string ToString() => $"solution-type: {Type.ToString()}; link: {Link}";
}