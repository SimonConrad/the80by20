using the80by20.App.MasterData.CategoryCrud;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.ReadModel;

// port
// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[Port]
[ReadModelDdd]
public interface ISolutionToProblemReadModelQueries
{
    Task<Category[]> GetProblemsCategories();

    IEnumerable<SolutionType> GetSolutionElementTypes();

    // todo creates feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and administration data-sources 
    Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id);

    // todo creates feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and administration data-sources 
    Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id);


}