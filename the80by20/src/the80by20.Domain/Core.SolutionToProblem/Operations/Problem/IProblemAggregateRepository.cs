using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

[Port]
[AggregateRepositoryDdd]
public interface IProblemAggregateRepository
{
    Task Create(ProblemAggregate aggregate, ProblemCrudData crudData);
    Task<ProblemAggregate> Get(ProblemId id);
    Task<ProblemCrudData> GetCrudData(ProblemId id);
    Task SaveAggragate(ProblemAggregate aggregate);
    Task SaveData(ProblemCrudData crudData);
}