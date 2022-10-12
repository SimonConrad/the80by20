using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;

namespace the80by20.Solution.Domain.SolutionToProblem.Capabilities;

[ValueObjectDdd]
public sealed record SolutionElement
{
    public SolutionType Type { get; }
    public string Link { get; }

    private SolutionElement(SolutionType type, string link)
    {
        Type = type;
        Link = link;
    }

    public static SolutionElement From(SolutionType type, string link) => new(type, link);

    public override string ToString() => $"solution-type: {Type.ToString()}; link: {Link}";
}