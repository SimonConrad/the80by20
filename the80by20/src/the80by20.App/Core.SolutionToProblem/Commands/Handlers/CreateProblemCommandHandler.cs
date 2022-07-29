using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.Core.SolutionToProblem.Operations;

namespace the80by20.App.Core.SolutionToProblem.Commands.Handlers;

public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _repository;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public CreateProblemCommandHandler(ISolutionToProblemAggregateRepository repository,
        IServiceScopeFactory servicesScopeFactory)
    {
        _repository = repository;
        _servicesScopeFactory = servicesScopeFactory;
    }

    // INFO application logic - coordinates flow + cross cuttings:
    // wrap with db transaction - handler decorator or aspect oriented
    // wrap with try catch logger
    public async Task<SolutionToProblemId> Handle(CreateProblemCommand command, CancellationToken cancellationToken)
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

        // todo retrieve from current user and do in more sophisticated way, like in ef on save like readonly property
        solutionToProblemCrudData.SetUser(Guid.NewGuid());

        await _repository.Create(solutionToProblemAggregate, solutionToProblemCrudData);

        // INFO done in FireAndForget way to present updating flow of CQRS read-model,
        // after command chnaged state of the system, read-model is updated in the background,
        // in future apply message queue (ex rabbit mq)
        UpdateReadModel(_servicesScopeFactory, solutionToProblemAggregate.Id);

        return solutionToProblemAggregate.Id;
    }

    public void UpdateReadModel(IServiceScopeFactory servicesScopeFactory, SolutionToProblemId id)
    {
        _ = Task.Run(async () =>
        {
            using var scope = servicesScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Publish(new ProblemCreated(id));
        });
    }


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
}