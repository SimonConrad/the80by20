using Common.DDD;
using Core.Domain.Core.SolutionToProblem.Operations;
using Core.Domain.SharedKernel.Capabilities;
using MediatR;

namespace Core.App.Core.SolutionToProblem.Commands;

// todo make record
[CommandDdd]
public class CreateProblemCommand : IRequest<SolutionToProblemId>
{
    public string Description { get; set; }
    
    public string DescriptionLinks { get; set; }

    public Guid Category { get; set; }

    public SolutionElementType[] SolutionElementTypes { get; set; }
}