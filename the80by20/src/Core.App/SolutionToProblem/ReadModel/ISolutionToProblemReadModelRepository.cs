using Common.DDD;
using Core.App.Administration;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Operations;

namespace Core.App.SolutionToProblem.ReadModel;

// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[ReadModelDdd]
public interface ISolutionToProblemReadModelRepository
{
    Task<Category[]> GetProblemsCategories();

    IEnumerable<SolutionElementType> GetSolutionElementTypes();

    public Task Create(SolutionToProblemReadModel readModel);

    public Task Update(SolutionToProblemReadModel readModel);

    Task<SolutionToProblemReadModel> Get(SolutionToProblemId id);
}