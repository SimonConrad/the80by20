﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.Commands.ProblemCommands;
using the80by20.Solution.App.Events.ProblemEvents;
using the80by20.Solution.Domain.Operations;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.App.CommandsHandlers.ProblemHandlers;

[CommandDdd]
public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _repository;
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public CreateProblemCommandHandler(
        IProblemAggregateRepository repository,
        IMediator mediator,
        IServiceScopeFactory servicesScopeFactory)
    {
        _repository = repository;
        _mediator = mediator;
        _servicesScopeFactory = servicesScopeFactory;
    }

    // INFO application logic - coordinates flow + cross cuttings:
    // todo wrap with db transaction - handler decorator or aspect oriented - but maybe problem with fire and forget updatereadmodel
    // todo wrap with try catch logger, and logger informaing about command received
    public async Task<ProblemId> Handle(CreateProblemCommand command, CancellationToken cancellationToken)
    {
        // INFO Creation of the aggregate
        // INFO Domain logic (have in mind different levels of domain logic)
        var problemAggregate = ProblemAggregate.New(RequiredSolutionTypes.From(command.SolutionElementTypes));

        ProblemCrudData problemCrudData =
            new(problemAggregate.Id, command.UserId, DateTime.Now, command.Description, command.Category);

        await _repository.Create(problemAggregate, problemCrudData);

        await UpdateReadModel(problemAggregate.Id, cancellationToken);

        return problemAggregate.Id;
    }

    public async Task UpdateReadModel(ProblemId id, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new ProblemCreated(id), cancellationToken);
    }

    // TODO found solution on .net docs that to do fire and forget do _ = Task.Run and method is not async,
    // however beacouse of problem with testing this (test application created via WebApplicationFactory<Program> is disposed before insides of taks.run) not using this
    // in future use messaging system on differnet process
    // https://stackoverflow.com/a/65577936

    // INFO done in FireAndForget way to present updating flow of CQRS read-model,
    // after command chnaged state of the system, read-model is updated in the background,
    // in future apply message queue (ex rabbit mq + convey wrapper)
    public void UpdateReadModelFireAndForget(IServiceScopeFactory servicesScopeFactory, ProblemId id)
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


// INFO input validation logic, do not check db there it's reposoibility of application logic