using Chronicle;
using the80by20.Saga.Messages;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Saga;

// TODO test saga
internal class Handler : IEventHandler<ProductCreated>, 
    IEventHandler<ProductsAssignedToClient>, 
    IEventHandler<ClientCreated>,
    IEventHandler<SolutionArchived>
{
    private readonly ISagaCoordinator _coordinator;

    public Handler(ISagaCoordinator coordinator)
        => _coordinator = coordinator;

    public Task HandleAsync(ProductCreated @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(ClientCreated @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    
    public Task HandleAsync(ProductsAssignedToClient @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    
    public Task HandleAsync(SolutionArchived @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
}