using the80by20.Modules.Masterdata.App.Services;
using the80by20.Modules.Solution.Domain.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.App.ReadModel;

//IEventHandler
public class ProblemCreatedEventHandler : IDomainEventHandler<ProblemCreated>
{
    private readonly ISolutionToProblemReadModelUpdates _readModelUpdates;
    private readonly ISolutionToProblemReadModelQueries _readModelQueries;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ICategoryService _categoryService;

    public ProblemCreatedEventHandler(ISolutionToProblemReadModelUpdates readModelUpdates,
        ISolutionToProblemReadModelQueries readModelQueries,
        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ICategoryService categoryService)
    {
        _readModelUpdates = readModelUpdates;
        _readModelQueries = readModelQueries;
        _problemAggregateRepository = problemAggregateRepository;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _categoryService = categoryService;
    }

    public async Task HandleAsync(ProblemCreated @event)
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
}