using Common.DDD;

namespace Core.Domain.SolutionToProblem.Capabilities;

[ValueObjectDdd]
public class SolutionAbstract 
{
    public string Content { get; init; }

    public static SolutionAbstract FromContent(string content) => new() { Content = content };

    public bool NotEmpty() => !string.IsNullOrEmpty(Content);
}