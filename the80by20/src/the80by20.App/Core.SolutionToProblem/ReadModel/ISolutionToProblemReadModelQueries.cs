using the80by20.App.Administration.MasterData;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.ReadModel;

// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[ReadModelDdd]
public interface ISolutionToProblemReadModelQueries
{
    Task<Category[]> GetProblemsCategories();

    IEnumerable<SolutionElementType> GetSolutionElementTypes();

    Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id);

    Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id);
}