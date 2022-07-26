using Common.DDD;
using Core.App.SolutionToProblem.Events;
using Core.Domain.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.SolutionToProblem.ReadModel;

[ReadModelDdd]
public class SolutionToProblemReadModelHandler : INotificationHandler<ProblemCreated>, INotificationHandler<ProblemUpdated>
{
    private readonly ISolutionToProblemAggregateRepository _aggregateRepository;
    private readonly ISolutionToProblemReadModelRepository _readModelRepository;

    public SolutionToProblemReadModelHandler(ISolutionToProblemAggregateRepository aggregateRepository,
        ISolutionToProblemReadModelRepository readModelRepository)
    {
        _aggregateRepository = aggregateRepository;
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(ProblemCreated problemCreated, CancellationToken cancellationToken)
    {
        var aggregate = await _aggregateRepository.Get(problemCreated.SolutionToProblemId);
        var data = await _aggregateRepository.GetCrudData(problemCreated.SolutionToProblemId);

        // info denormalized view consisting of projection of aggregate invariant attributes, related to aggregate crud data and others
        // dedicated for command deciding to do, based on es model
        var readmodel = new SolutionToProblemReadModel()
        {
            SolutionToProblemId = data.AggregateId,
            UserId = data.UserId,
            RequiredSolutionElementTypes = string.Join("--", aggregate.RequiredSolutionElementTypes.Elements.Select(t => t.ToString()).ToArray()),
            Description = data.Description,

            IsConfirmed = aggregate.Confirmed,
            IsRejected = aggregate.Rejected,
            WorkingOnSolutionStarted = aggregate.WorkingOnSolutionStarted,
            WorkingOnSolutionEnded = aggregate.WorkingOnSolutionEnded,
                
            Price = aggregate.Price,
            SolutionAbstract = aggregate.SolutionAbstract.Content,
            SolutionElementTypes = string.Join("--", aggregate.SolutionElements.Elements.Select(t => t.ToString()).ToArray()),

            Category = problemCreated.Category, // info not stored for invariants in aggragate nor in crud anemic eneity data related to aggregate as wont be chnaged,
            
            CreatedAt = DateTime.Now
        };

        await _readModelRepository.Create(readmodel);
    }

    public Task Handle(ProblemUpdated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}