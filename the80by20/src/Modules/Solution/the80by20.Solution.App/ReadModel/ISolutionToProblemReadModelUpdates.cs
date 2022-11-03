using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.ReadModel;

// port
// INFO port in hexagon arch, its adapter is in dal this is IoC
// - so that app layer do not relay on dal layer, project depnedncy direction is opposite to the direction of program exceution flow 
[ReadModelDdd]
public interface ISolutionToProblemReadModelUpdates
{
    public Task Create(SolutionToProblemReadModel readModel);

    public Task Update(SolutionToProblemReadModel readModel);
}