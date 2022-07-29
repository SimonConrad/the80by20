using Common.DDD;
using Core.App.Administration.MasterData;
using Core.Domain.Core.SolutionToProblem.Operations;
using Core.Domain.SharedKernel.Capabilities;

namespace Core.App.Core.SolutionToProblem.ReadModel;

// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[ReadModelDdd]
public interface ISolutionToProblemReadModelQueries
{
    Task<Category[]> GetProblemsCategories();

    IEnumerable<SolutionElementType> GetSolutionElementTypes();

    Task<SolutionToProblemReadModel> Get(SolutionToProblemId id);
}