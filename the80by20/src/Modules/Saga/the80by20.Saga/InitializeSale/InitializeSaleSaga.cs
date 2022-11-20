using Chronicle;
using the80by20.Saga.Messages;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Saga.InitializeSale;

[Saga]

// TODO how to persist in database?
internal class InitializeSaleSaga : Saga<InitializeSaleSaga.SagaData>,     // INFO alternative instead of chronicle can use mas transit
    ISagaStartAction<ProductCreated>, // todo consider starting with solution-finished event so tha t saga will span over two modules solution and sale, then chnage ProductCreated to saga action
    ISagaAction<ClientCreated>,
    ISagaAction<ProductsAssignedToClient>
{
    private readonly IModuleClient _moduleClient;
    private readonly IMessageBroker _messageBroker;
    
    // INFO todo update description
    // 1. Received event ProductCreated from Sale module
    //      published by the80by20.Modules.Sale.App.Events.External.Handlers.SolutionFinishedSaleHandler
    // 2. Send command ArchiveSolution to Solution module
    // 3. Send command CreateClient to Sale module
    //      same identity (snowflake id) as user, but different meaning - because user in users bounded contexts and client in sale
    //      bounded ctxt, different bounded contexts operates on different user/client attributes, behaviors)
    // 4. query GetNumberOfProductsBoughtByClient by sale-api-client - query to Sale module
    // 5. Based on returns from 4 do logic of counting % discount and send command ApplyDiscountToProduct to Sale module

    // compensation - apply appropriate rollbacks if any of commands: 2,3,5 fails
    // client is not user (different context - different abstraction - different scope of attributes and behaviors,
    // but share one identity)

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            ProductCreated m => m.UserId.ToString(),
            ClientCreated m => m.ClientId.ToString(),
            ProductsAssignedToClient m => m.ClientId.ToString(),
            _ => base.ResolveId(message, context)
        };

    public InitializeSaleSaga(IModuleClient moduleClient, IMessageBroker messageBroker)
    {
        _moduleClient = moduleClient;
        _messageBroker = messageBroker;
    }

    internal class SagaData
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool ClientCreated { get; set; }
        
        public bool UserIsAlreadyClient { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }

    public async Task HandleAsync(ProductCreated message, ISagaContext context)
    {
        var (ProductId, UserId) = message;
        // TODO check if user already is our client if so UserIsAlreadyClient = true and instead of clients/create send update
        Data.UserId = UserId;
        Data.ProductId = ProductId;

        await _moduleClient.SendAsync("clients/create", new { UserId = UserId, ProductId = ProductId });

        return; //todo when to use  await CompleteAsync();
    }

    public Task HandleAsync(ClientCreated message, ISagaContext context)
    {
        Data.ClientCreated = true;
        return Task.CompletedTask;
        
    }
    public async Task HandleAsync(ProductsAssignedToClient message, ISagaContext context)
    {
        if (Data.ClientCreated)
        {
            // todo send async finish-solution using _messageBroker
            await _messageBroker.PublishAsync(new SendSaleDetailsMessage(Data.Email, Data.FullName));
            await CompleteAsync();
        }
    }



    public Task CompensateAsync(ProductCreated message, ISagaContext context) => Task.CompletedTask;
    public Task CompensateAsync(ProductsAssignedToClient message, ISagaContext context) => Task.CompletedTask; //todo
    public Task CompensateAsync(ClientCreated message, ISagaContext context) => Task.CompletedTask; //todo
}