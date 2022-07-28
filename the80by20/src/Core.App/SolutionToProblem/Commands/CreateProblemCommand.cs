using Core.Domain.SharedKernel.Capabilities;

namespace Core.App.SolutionToProblem.Commands;

// todo make record
public class CreateProblemCommand
{
    public string Description { get; set; }
    
    public string DescriptionLinks { get; set; }

    public Guid Category { get; set; }

    public SolutionElementType[] SolutionElementTypes { get; set; }
}