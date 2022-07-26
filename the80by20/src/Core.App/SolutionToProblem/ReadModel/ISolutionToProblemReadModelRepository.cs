using Common.DDD;
using Core.Domain.SolutionToProblem.Operations;

namespace Core.App.SolutionToProblem.ReadModel;

// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[ReadModelDdd]
public interface ISolutionToProblemReadModelRepository
{
    Task<SolutionToProblemReadModel> Get(SolutionToProblemId id);

    Task Create(SolutionToProblemReadModel readModel);

    Task Update(SolutionToProblemReadModel model);
    Task<SolutionToProblemAggregate> GetAggregate(SolutionToProblemId id);
    Task<SolutionToProblemCrudData> GetAggregateCrudData(SolutionToProblemId id);
}