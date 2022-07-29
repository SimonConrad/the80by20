using MediatR;
using the80by20.App.Administration.MasterData;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations;

namespace the80by20.App.Core.SolutionToProblem.ReadModel;

[ReadModelDdd]
public class SolutionToProblemReadModelEventHandler : INotificationHandler<ProblemCreated>, INotificationHandler<ProblemUpdated>
{
    private readonly ISolutionToProblemReadModelUpdates _readModelRepository;
    private readonly ISolutionToProblemAggregateRepository _aggregateRepository;
    private readonly ICategoryCrudRepository _categoryCrudRepository;

    public SolutionToProblemReadModelEventHandler(
        ISolutionToProblemReadModelUpdates readModelRepository,
        ISolutionToProblemAggregateRepository aggregateRepository,
        ICategoryCrudRepository categoryCrudRepository)
    {
        _readModelRepository = readModelRepository;
        _aggregateRepository = aggregateRepository;
        _categoryCrudRepository = categoryCrudRepository;
    }

    public async Task Handle(ProblemCreated problemCreated, CancellationToken cancellationToken)
    {
        var aggregate = await _aggregateRepository.Get(problemCreated.SolutionToProblemId);
        var data = await _aggregateRepository.GetCrudData(problemCreated.SolutionToProblemId);
        
        // TODO set proper user-id
        var category = await _categoryCrudRepository.GetById(Guid.Parse("00000000-0000-0000-0000-000000000004"));

        var readmodel = new SolutionToProblemReadModel()
        {
            SolutionToProblemId = data.AggregateId,
            UserId = data.UserId,
            RequiredSolutionElementTypes = string.Join("--", aggregate.RequiredSolutionElementTypes.Elements.Select(t => t.ToString()).ToArray()),
            Description = data.Description,
            DescriptionLinks = data.DescriptionLinks,

            IsConfirmed = aggregate.Confirmed,
            IsRejected = aggregate.Rejected,
            WorkingOnSolutionStarted = aggregate.WorkingOnSolutionStarted,
            WorkingOnSolutionEnded = aggregate.WorkingOnSolutionEnded,
                
            Price = aggregate.Price,
            SolutionAbstract = aggregate.SolutionAbstract.Content,
            SolutionElementTypes = string.Join("--", aggregate.SolutionElements.Elements.Select(t => t.ToString()).ToArray()),

            Category = category.Name,
            
            CreatedAt = DateTime.Now
        };

        await _readModelRepository.Create(readmodel);
    }

    public Task Handle(ProblemUpdated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}