using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.App.SolutionToProblem.Reads;

// TODO make as C#'s record
[ReadModelDdd]
public class SolutionToProblemReadModel
{
    public Guid SolutionToProblemId { get; set; }

    public Guid UserId { get; set; }

    public string Description { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsRejected { get; set; }
    
    public bool WorkingOnSolutionStarted { get; set; }

    public bool WorkingOnSolutionEnded { get; set; }

    public SolutionAbstract SolutionAbstract { get; set; } // TODO proper mapping for webapi response model - do in webapi project

    public Price Price { get; set; } // TODO proper mapping for webapi response model - do in webapi project
    
}