using MediatR;
using the80by20.App.Administration.MasterData;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.ReadModel;

/// <summary>
/// TODO: in production rather do alternative to this read model handling problems / solutions updates mechanism
/// is just use readmodel queries that do  projection (returns SolutionToProblemReadModel) composed from data retrieved from
/// aggregate repos and administration category crud
/// </summary>
[ReadModelDdd]
public class SolutionToProblemReadModelEventHandler : INotificationHandler<ProblemCreated>, INotificationHandler<ProblemUpdated>
{
    private readonly ISolutionToProblemReadModelUpdates _readModelUpdates;
    private readonly ISolutionToProblemReadModelQueries _readModelQueries;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ICategoryCrudRepository _categoryCrudRepository;

    public SolutionToProblemReadModelEventHandler(
        ISolutionToProblemReadModelUpdates readModelUpdates,
        ISolutionToProblemReadModelQueries readModelQueries,

        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ICategoryCrudRepository categoryCrudRepository)
    {
        _readModelUpdates = readModelUpdates;
        _readModelQueries = readModelQueries;
        _problemAggregateRepository = problemAggregateRepository;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _categoryCrudRepository = categoryCrudRepository;
    }

    public async Task Handle(ProblemCreated problemCreated, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(problemCreated.ProblemId);
        var problemData = await _problemAggregateRepository.GetCrudData(problemCreated.ProblemId);
        var category = await _categoryCrudRepository.GetById(problemData.Category);

        var readmodel = new SolutionToProblemReadModel()
        {
            ProblemId = problem.Id,
            RequiredSolutionElementTypes = string.Join("--", problem.RequiredSolutionTypes.Elements.Select(t => t.ToString()).ToArray()),
            IsConfirmed = problem.Confirmed,
            IsRejected = problem.Rejected,

            Description = problemData.Description,
            UserId = problemData.UserId,
            CreatedAt = problemData.CreatedAt,
            Category = category.Name,
        };

        await _readModelUpdates.Create(readmodel);
    }

    public async Task Handle(ProblemUpdated problemUpdated, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(problemUpdated.ProblemId);
        var problemData = await _problemAggregateRepository.GetCrudData(problemUpdated.ProblemId);
        var category = await _categoryCrudRepository.GetById(problemData.Category);

        var rm = await _readModelQueries.GetByProblemId(problemUpdated.ProblemId);

        rm.RequiredSolutionElementTypes =
            string.Join("--", problem.RequiredSolutionTypes.Elements.Select(t => t.ToString()).ToArray());
        rm.IsConfirmed = problem.Confirmed;
        rm.IsRejected = problem.Rejected;
        rm.Description = problemData.Description;
        rm.Category = category.Name;
    }

    // todo solutioncreated - pobierze po problem id readmodel i doda wartości w nowego garegat solution
    // todo solution updated - zaktualizuje readmodel analogicznie jak dla problemupdated, ale danymi solution
}