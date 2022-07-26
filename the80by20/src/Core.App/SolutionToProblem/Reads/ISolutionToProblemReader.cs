using Common.DDD;
using Core.Domain.SolutionToProblem.Operations;

namespace Core.App.SolutionToProblem.Reads;

public interface ISolutionToProblemReader
{
    [ReadModelDdd] // INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
    Task<SolutionToProblemReadModel> Get(SolutionToProblemId id);
}