using Chronicle;
using the80by20.Saga.Messages;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Saga.InitializeSale;


// TODO
// how to persist in database?
// read chronicle docs

// INFO
// alternative instead of Chronicle https://masstransit-project.com/ can be used
// docs: https://github.com/snatch-dev/Chronicle

// INFO 
// 1. Received event ProductCreated from Sale module
//      published by the80by20.Services.Sale.App.Events.External.Handlers.SolutionFinishedSaleHandler
// 2. Send command create-client to Sale module
//      why create new "user"?, beacouse in sale module user is not *user* but it is *client* - it has same identity (snowflake id) as user,
//      but different set of attributes and behaviors - user in users bounded context and client in sale
//      bounded context  operates on different user/client attributes, behaviors - it can be discovered during event storming session
//      by using ubiquitous language technique
// 3. Send command assign-product-to-client to Sale module
//      (custom logic of assigning discount to user based on its products hisotry is done in sale module in this command handler not place it in saga
// 4. Send command archive-solution to Solution module

// - compensation - apply appropriate rollbacks if any of commands: 2,3,5 fails detials in chronicle docs
// - client is not user (different context - different abstraction - different scope of attributes and behaviors, but share one identity)

[Saga]

internal class InitializeSaleSaga : Saga<InitializeSaleSaga.SagaData>,    
    ISagaStartAction<ProductCreated>, 
    ISagaAction<ClientCreated>,
    ISagaAction<ProductsAssignedToClient>,
    ISagaAction<SolutionArchived>
{
    private readonly IModuleClient _moduleClient;
    private readonly IMessageBroker _messageBroker;
    
    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            ProductCreated m => m.UserId.ToString(),
            ClientCreated m => m.ClientId.ToString(),
            ProductsAssignedToClient m => m.ClientId.ToString(),
            SolutionArchived m => m.UserId.ToString(),
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
        
        public bool ProductsAssignedToClient { get; set; }
        
        public bool SolutionArchived { get; set; }
        
        public bool UserIsAlreadyClient { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }

    public async Task HandleAsync(ProductCreated message, ISagaContext context)
    {
        var (ProductId, UserId) = message;
        
        Data.UserId = UserId;
        Data.ProductId = ProductId;

        // INFO
        // if using separate web-services instead od modular-monolith then send command using : httpfactory + httpclient
        // if want asynchronously use _messageBroker.PublishAsync
        await _moduleClient.SendAsync("sale/clients/create", new { UserId = UserId }); // TODO expose api in sale module

    }

    public async Task HandleAsync(ClientCreated message, ISagaContext context)
    {
        Data.ClientCreated = true;
        
        // INFO
        // if using separate web-services instead od modular-monolith then send command using : httpfactory + httpclient
        // if want asynchronously use _messageBroker.PublishAsync
        await _moduleClient.SendAsync("sale/products/assign-product-to-client", // TODO expose api in sale module
            new { UserId = Data.UserId, ProductId  = Data.ProductId});
    }
    
    public async Task HandleAsync(ProductsAssignedToClient message, ISagaContext context)
    {
        Data.ProductsAssignedToClient = true;
        
        if (Data.ClientCreated)
        {
            await _moduleClient.SendAsync("solution/solution/archive-solution", // TODO expose api in solution module
                new { UserId = Data.UserId, ProductId  = Data.ProductId});
        }
    }

    public async Task HandleAsync(SolutionArchived message, ISagaContext context)
    {
        if (Data.ClientCreated && Data.ProductsAssignedToClient)
        {
            await _messageBroker.PublishAsync(new SendSaleDetailsMessage(Data.Email, Data.FullName));
            
            await CompleteAsync();
        }

        return;
    }

    public Task CompensateAsync(ProductCreated message, ISagaContext context) => Task.CompletedTask;
    public Task CompensateAsync(ProductsAssignedToClient message, ISagaContext context) => Task.CompletedTask; //todo
    public Task CompensateAsync(ClientCreated message, ISagaContext context) => Task.CompletedTask; //todo
    public Task CompensateAsync(SolutionArchived message, ISagaContext context) => Task.CompletedTask; //todo

}