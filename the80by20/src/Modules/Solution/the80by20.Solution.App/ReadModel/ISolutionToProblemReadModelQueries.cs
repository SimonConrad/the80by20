using the80by20.Modules.Masterdata.App.DTO;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.ReadModel;

// port
// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[Port]
[ReadModelDdd]
public interface ISolutionToProblemReadModelQueries
{
    Task<CategoryDto[]> GetProblemsCategories();

    IEnumerable<SolutionType> GetSolutionElementTypes();

    // todo create feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and master-data data-sources 
    Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id);

    // todo create feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and master-data data-sources 
    Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id);

    Task<SolutionToProblemReadModel[]> GetByUserId(Guid userId);
}