using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionAbstract 
{
    public string Content { get; init; }

    public static SolutionAbstract FromContent(string content) => new() { Content = content };

    public bool NotEmpty() => !string.IsNullOrEmpty(Content);
}