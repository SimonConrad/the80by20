using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Saga.InitializeSale;

[Saga]
public class InitializeSaleSaga
{
    // INFO
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
}