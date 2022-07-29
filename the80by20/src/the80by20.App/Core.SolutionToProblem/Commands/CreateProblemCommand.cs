using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands;

// todo make record
[CommandDdd]
public class CreateProblemCommand : IRequest<SolutionToProblemId>
{
    public string Description { get; set; }
    
    public string DescriptionLinks { get; set; }

    public Guid Category { get; set; }

    public SolutionElementType[] SolutionElementTypes { get; set; }
}