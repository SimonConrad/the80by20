using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Solution.ValueObjects;

[ValueObjectDdd]
public sealed record SolutionSummary
{
    public string Content { get; }

    public static SolutionSummary FromContent(string content)
    {
        if (string.IsNullOrEmpty(content) || content.Length < 10)
        {
            throw new DomainException(nameof(SolutionSummary));
        }

        return new(content);
    }

    public static SolutionSummary Empty() => new(string.Empty);

    private SolutionSummary(string content)
    {
        Content = content;
    }

    public bool IsEmpty() => string.IsNullOrEmpty(Content);

    public override string ToString() => $"{Content.Substring(10)} ...";
}