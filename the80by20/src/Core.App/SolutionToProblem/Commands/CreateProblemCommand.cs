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
        
    // TODO Create VO Category
    public string Category { get; set; }

    public Guid UserId { get; set; }

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
            UserId = command.UserId,
            Category = command.Category,
            Description = command.Description
        };

        await _repository.Create(solutionToProblemAggregate, solutionToProblemCrudData);

        // info fire event and forget asynchronously using task api, without awaiting result
        // todo interchnage with messagin mechanism like:g rabbitmq, nservicebys, kafka masstransit, create ibnterfaces for that
        // https://github.com/jbogard/MediatR/discussions/736 publishing startegy
       /* await*/ Task.Run(() =>
        {
            _mediator.Publish(new ProblemCreated(solutionToProblemAggregate.Id, command.Category));
        });

        return solutionToProblemAggregate.Id;
    }
}