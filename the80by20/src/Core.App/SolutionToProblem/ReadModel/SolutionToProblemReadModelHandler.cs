using Common.DDD;
using Core.App.SolutionToProblem.Events;
using MediatR;

namespace Core.App.SolutionToProblem.ReadModel;

[ReadModelDdd]
public class SolutionToProblemReadModelHandler : INotificationHandler<ProblemCreated>, INotificationHandler<ProblemUpdated>
{
    private readonly ISolutionToProblemReadModelRepository _readModelRepository;

    public SolutionToProblemReadModelHandler(ISolutionToProblemReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(ProblemCreated problemCreated, CancellationToken cancellationToken)
    {
        var aggregate = await _readModelRepository.GetAggregate(problemCreated.SolutionToProblemId);
        var data = await _readModelRepository.GetAggregateCrudData(problemCreated.SolutionToProblemId);

        // info denormalized (optimized for fast reads, and scope od data read)view consisting of projection of:
        // aggregate invariant attributes, related to aggregate crud data, and others
        // dedicated for command deciding to do, based on es model
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