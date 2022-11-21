using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Sale.App.Events
{

    [IntegrationEvent]
    public record ProductCreated(Guid ProductId, Guid UserId) : IEvent;
}