using Common.DDD;

namespace Core.App.Core.SolutionToProblem.ReadModel;

// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[ReadModelDdd]
public interface ISolutionToProblemReadModelUpdates
{
    public Task Create(SolutionToProblemReadModel readModel);

    public Task Update(SolutionToProblemReadModel readModel);
}