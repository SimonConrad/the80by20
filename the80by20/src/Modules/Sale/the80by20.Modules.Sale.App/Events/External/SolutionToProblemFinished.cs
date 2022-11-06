using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Sale.App.Events.External
{
    public record SolutionToProblemFinished(
        Guid solutionId,
        Guid userThatRequestedSolution,
        string SolutionSummary,
        string SolutionElementsGDriveLink,
        decimal price) : IEvent;

}