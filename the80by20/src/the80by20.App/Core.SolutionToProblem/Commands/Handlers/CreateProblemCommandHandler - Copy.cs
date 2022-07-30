using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

namespace the80by20.App.Core.SolutionToProblem.Commands.Handlers;

public class UpdateProblemCommandHandler : IRequestHandler<UpdatProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _repository;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public UpdateProblemCommandHandler(
        IProblemAggregateRepository repository,
        IServiceScopeFactory servicesScopeFactory)
    {
        _repository = repository;
        _servicesScopeFactory = servicesScopeFactory;
    }
    public async Task<ProblemId> Handle(UpdatProblemCommand command, CancellationToken cancellationToken)
    {


        //_repository.Get(command.)


        var problemAggregate = ProblemAggregate.New(
            RequiredSolutionElementTypes.From(command.SolutionElementTypes),
            command.Description, 
            command.Category);

        // todo retrieve from current user and do in more sophisticated way, like in ef on save like readonly property
        var userId = Guid.NewGuid();
        ProblemCrudData problemCrudData =
            new(problemAggregate.Id, userId, DateTime.Now, command.Description, command.Category);

        await _repository.Create(problemAggregate, problemCrudData);

        // INFO done in FireAndForget way to present updating flow of CQRS read-model,
        // after command chnaged state of the system, read-model is updated in the background,
        // in future apply message queue (ex rabbit mq)
        UpdateReadModel(_servicesScopeFactory, problemAggregate.Id);

        return problemAggregate.Id;
    }

    public void UpdateReadModel(IServiceScopeFactory servicesScopeFactory, ProblemId id)
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