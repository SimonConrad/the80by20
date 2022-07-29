using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Capabilities;

[ValueObjectDdd]
public sealed record SolutionAbstract 
{
    public string Content { get; }

    public static SolutionAbstract FromContent(string content)
    {
        if (string.IsNullOrEmpty(content) || content.Length < 10)
        {
            throw new DomainException(nameof(SolutionAbstract));
        }

        return new(content);
    }

    public static SolutionAbstract Empty() => new(string.Empty);

    private  SolutionAbstract(string content)
    {
        Content = content;
    }

    public bool NotEmpty() => !string.IsNullOrEmpty(Content);

    public override string ToString() => $"{Content.Substring(10)} ...";
}