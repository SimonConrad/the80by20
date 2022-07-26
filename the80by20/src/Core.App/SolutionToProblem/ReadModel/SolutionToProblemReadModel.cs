using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.App.SolutionToProblem.ReadModel;

// info denormalized (in db) model, optimized for fast queries without unecessary joins, on event storming model a decision data for doing or not command
// updated in db always as a a reaction that event happened, so every data, updated in commands catalog,
// if we have crud logic as in admisnitaration where will be jus generic crud repo there 
// in case of event sourcing also done this way
[ReadModelDdd]
public class SolutionToProblemReadModel
{
    public Guid SolutionToProblemId { get; set; }

    public Guid UserId { get; set; }

    public string RequiredSolutionElementTypes { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }
    
    public bool IsConfirmed { get; set; }

    public bool IsRejected { get; set; }

    public bool WorkingOnSolutionStarted { get; set; }

    public bool WorkingOnSolutionEnded { get; set; }

    public decimal Price { get; set; }

    public string SolutionAbstract { get; set; }

    public string SolutionElementTypes { get; set; }

    public DateTime CreatedAt { get; set; }
}