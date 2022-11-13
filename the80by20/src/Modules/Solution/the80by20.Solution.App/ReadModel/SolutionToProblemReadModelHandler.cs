using MediatR;
using the80by20.Modules.Masterdata.App.Services;
using the80by20.Modules.Solution.App.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.App.ReadModel;

/// <summary>
/// TODO: in production rather do alternative to this read model handling problems / solutions updates mechanism
/// is just use readmodel queries that do  projection (returns SolutionToProblemReadModel) composed from data retrieved from
/// aggregate repos and administration category crud
/// </summary>
/// 

// INFO
// Dedicated read model storing data in unnormalized table optimized for fast reads
// it is not immediatly consistent with aggregata data but eventually consistent
[ReadModelDdd]
public class ProblemReadModelHandler :
    IDomainEventHandler<ProblemRequested>,


    INotificationHandler<ProblemUpdated>
{
    private readonly ISolutionToProblemReadModelUpdates _readModelUpdates;
    private readonly ISolutionToProblemReadModelQueries _readModelQueries;

    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ICategoryService _categoryService;

    public ProblemReadModelHandler(ISolutionToProblemReadModelUpdates readModelUpdates,
        ISolutionToProblemReadModelQueries readModelQueries,
        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ICategoryService categoryService)
    {
        _readModelUpdates = readModelUpdates;
        _readModelQueries = readModelQueries;
        _problemAggregateRepository = problemAggregateRepository;
        _categoryService = categoryService;
    }

    public async Task HandleAsync(ProblemRequested @event)
    {
        var problem = await _problemAggregateRepository.Get(@event.problem.Id.Value);
        var problemData = await _problemAggregateRepository.GetCrudData(@event.problem.Id.Value);
        var category = await _categoryService.GetAsync(problemData.Category);

        var readmodel = new SolutionToProblemReadModel()
        {
            Id = problem.Id,
            RequiredSolutionTypes = string.Join("--", problem.RequiredSolutionTypes.Elements.Select(t => t.ToString()).ToArray()),
            IsConfirmed = problem.Confirmed,
            IsRejected = problem.Rejected,

            Description = problemData.Description,
            UserId = problemData.UserId,
            CreatedAt = problemData.CreatedAt,
            Category = category.Name,
            CategoryId = category.Id,
        };

        await _readModelUpdates.Create(readmodel);
    }

    public async Task Handle(ProblemUpdated @event, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(@event.ProblemId);
        var problemData = await _problemAggregateRepository.GetCrudData(@event.ProblemId);
        var category = await _categoryService.GetAsync(problemData.Category);

        var rm = await _readModelQueries.GetByProblemId(@event.ProblemId);

        rm.RequiredSolutionTypes =
            string.Join("--", problem.RequiredSolutionTypes.Elements.Select(t => t.ToString()).ToArray());
        rm.IsConfirmed = problem.Confirmed;
        rm.IsRejected = problem.Rejected;
        rm.Description = problemData.Description;
        rm.Category = category.Name;
        rm.CategoryId = category.Id;

        await _readModelUpdates.Update(rm);
    }
}
