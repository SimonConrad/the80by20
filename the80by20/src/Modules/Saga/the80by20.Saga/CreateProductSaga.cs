using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Saga;

[Saga]
public class CreateProductSaga
{
    // INFO
    // 1. Received event ProblemCreated from Solution module
    // 2. Send command CreateClient to Sale module (to start tracking potential client, smae identity
    // as user, but different meaning i deeferen context)
    // 3. Received event SolutionFinsihed (of ProductCreated)  from Solution module
    // 4. Send command ProblemIsProduct, SolutionIsProduct to Solution module
    // 5. Send command CreateProduct (based on Solution) to Sale module
    // 6. GetClients bought product - query to Sale
    // 7. Based on returns from 6 do logic of counting discounts and send command ApplyDicountToClient
    // (id of client from 2)
    
    // compenstatio when 5 fails rollback 4
    // client is not user (diffrenet context - diffrenet abstraction - diferent scope of attributes and behaviors,
    // but share 1 idenitity, so )
    // divide into 2 sagas?
}