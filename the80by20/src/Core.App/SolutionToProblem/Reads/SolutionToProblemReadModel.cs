using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.App.SolutionToProblem.Reads;

[ReadModelDdd]
public class SolutionToProblemReadModel
{
    public Guid SolutionToProblemId { get; set; }

    public Guid UserId { get; set; }

    public string[] RequiredSolutionElementTypes { get; set; }

    public string Description { get; set; }
    
    public bool IsConfirmed { get; set; }

    public bool IsRejected { get; set; }

    public bool WorkingOnSolutionStarted { get; set; }

    public bool WorkingOnSolutionEnded { get; set; }

    public Money Price { get; set; }

    public SolutionAbstract SolutionAbstract { get; set; }

    public string[] SolutionElementTypes { get; set; }
}