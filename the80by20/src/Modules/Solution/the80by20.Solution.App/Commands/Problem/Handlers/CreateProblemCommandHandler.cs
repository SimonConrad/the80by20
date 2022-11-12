using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.App.Commands.Problem.Handlers;


public class CreateProblemCommandHandler : ICommandHandler<CreateProblemCommand>
{
    private readonly IProblemAggregateRepository _repository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public CreateProblemCommandHandler(
        IProblemAggregateRepository repository,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _repository = repository;
        _domainEventDispatcher = domainEventDispatcher;
    }


    // INFO application logic - coordinates flow + cross cuttings
    public async Task HandleAsync(CreateProblemCommand command)
    {
        // INFO Creation of the aggregate
        // INFO Domain logic (have in mind different levels of domain logic)
        var problemAggregate = ProblemAggregate.New(command.Id, RequiredSolutionTypes.From(command.SolutionElementTypes));

        ProblemCrudData problemCrudData = new(problemAggregate.Id, command.UserId, DateTime.Now, command.Description, command.Category);

        await _repository.Create(problemAggregate, problemCrudData);

        await _domainEventDispatcher.DispatchAsync(problemAggregate.Events.ToArray());

        //await UpdateReadModel(problemAggregate.Id.Value);
    }

    //public async Task UpdateReadModel(ProblemId id)
    //{
    //    await _mediator.Publish(new ProblemCreated(id));
    //}

    // TODO found solution on .net docs that to do fire and forget do _ = Task.Run and method is not async,
    // however beacouse of problem with testing this (test application created via WebApplicationFactory<Program> is disposed before insides of taks.run) not using this
    // in future use messaging system on differnet process
    // https://stackoverflow.com/a/65577936

    // INFO done in FireAndForget way to present updating flow of CQRS read-model,
    // after command chnaged state of the system, read-model is updated in the background,
    // in future apply message queue (ex rabbit mq + convey wrapper)
    //public void UpdateReadModelFireAndForget(IServiceScopeFactory servicesScopeFactory, ProblemId id)
    //{
    //    _ = Task.Run(async () =>
    //    {
    //        using var scope = servicesScopeFactory.CreateScope();
    //        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    //        await mediator.Publish(new ProblemCreated(id));
    //    });
    //}

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


// INFO input validation logic, do not check db there it's reposoibility of application logic