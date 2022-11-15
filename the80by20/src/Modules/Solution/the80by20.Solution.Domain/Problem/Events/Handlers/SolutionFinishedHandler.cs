using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Solution.Events;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Problem.Events.Handlers
{
    internal sealed class SolutionFinishedHandler : IDomainEventHandler<SolutionFinished>
    {
        private readonly IProblemAggregateRepository _repository;

        public SolutionFinishedHandler(IProblemAggregateRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(SolutionFinished @event)
        {
            // INFO
            // Archive problem entity by soft delete
            // when solution (problem becamoe solution) is finished then oringinating it problem end its lifecycle

            // TODO
            // var problemAggregate = _repository.Get(@event.ProblemId)
            // problemAggregate.Archive()
            //_repository.Save(problemAggregate)
            return Task.FromResult(true);

        }
    }
}
