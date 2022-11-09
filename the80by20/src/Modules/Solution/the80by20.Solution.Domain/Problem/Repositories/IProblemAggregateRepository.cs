using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Problem.Repositories;

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