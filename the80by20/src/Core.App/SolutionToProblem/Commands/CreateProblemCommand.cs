using Core.App.SolutionToProblem.Events;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.SolutionToProblem.Commands;

// todo make record
public class CreateProblemCommand
{
    public string Description { get; set; }
    
    public string DescriptionLinks { get; set; }

    public string Category { get; set; }

    public SolutionElementType[] SolutionElementTypes { get; set; }
}

public class CreateProblemCommandHandler
{
    private readonly ISolutionToProblemAggregateRepository _repository;
    private readonly IMediator _mediator;

    public CreateProblemCommandHandler(ISolutionToProblemAggregateRepository repository,
        IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    // INFO application logic - coordinates flow + cross cuttings:
    // wrap with db transaction - handler decorator or aspect oriented
    // wrap with try catch logger
    public async Task<Guid> Handle(CreateProblemCommand command)
    {
        // INFO input validation logic
        // TODO FluentValidator on command

        var solutionToProblemAggregate = SolutionToProblemAggregate.New(
            RequiredSolutionElementTypes.From(command.SolutionElementTypes));

        SolutionToProblemCrudData solutionToProblemCrudData = new()
        {
            AggregateId = solutionToProblemAggregate.Id,
            Category = command.Category,
            Description = command.Description,
            DescriptionLinks = command.DescriptionLinks
        };
        
        solutionToProblemCrudData.SetUser(Guid.NewGuid()); // todo retrieve from current user and do in more sophisticated way, like in ef on save like readonly property

        await _repository.Create(solutionToProblemAggregate, solutionToProblemCrudData);

        // TODO:
        // info fire event and forget asynchronously using task api, without awaiting result, but this way problem with dbcontext injection
        // todo interchnage with messagin mechanism like:g rabbitmq, nservicebys, kafka masstransit, create ibnterfaces for that,
        // or maybe ihostedservice backround job will be suffcient or hangfire
        // https://github.com/jbogard/MediatR/discussions/736 publishing startegy

        // to achieve fire and forget event publishing we have this option in mediatr
        // this cannot use mechanism of injection of CoreDbContext into SolutionToProblemReadModelRepository,
        // as on other thread it is not availble, so for this purpose CoreDbContextFactory was created,
        // it handles special case in which sqllite is used, to make it perisistant its connection is singleton (static field)
        // sqlLiteEnabled in appsettings.json
        Task.Run(() =>
        {
            _mediator.Publish(new ProblemCreated(solutionToProblemAggregate.Id, command.Category));
        });

        return solutionToProblemAggregate.Id;
    }
}