using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Services.Sale.App.Events.External
{

    [IntegrationEvent]
    public record SolutionFinished(Guid solutionId) : IEvent;

}