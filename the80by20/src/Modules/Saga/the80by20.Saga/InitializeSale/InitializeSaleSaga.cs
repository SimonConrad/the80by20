using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Saga;

[Saga]
public class InitializeSaleSaga
{
    // INFO
    // 1. Received event ProblemCreated from Solution module
    // 2. Send command CreateClient to Sale module (to start tracking potential client, same identity (snowflake id)
    // as user, but different meaning - because  user in users bounded contexts and client in sale
    // bounded ctxt, different bounded contexts operates on different user/client attributes, behaviors)
    // 3. Received event SolutionFinished  from Solution module
    // 4. Send command/s ArchiveProblem, ArchiveSolution to Solution module
    // 5. Send command CreateProduct (based on Solution id) to Sale module, sale module read using solution-api-client
    // by id solution details
    // 6. query GetNumberOfProductsBought by client - query to Sale
    // 7. Based on returns from 6 do logic of counting % discount and send command ApplyDiscountToClient to Sale module
    // (id of client from 2)
    
    // compensation - when 5 fails rollback 4
    // client is not user (different context - different abstraction - different scope of attributes and behaviors,
    // but share one identity)
    // maybe divide into 2 sagas - 6 and 7 to separate saga?
}