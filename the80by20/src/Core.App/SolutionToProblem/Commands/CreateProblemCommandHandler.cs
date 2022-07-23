using Core.Domain.SolutionToProblem;

namespace Core.App.SolutionToProblem.Commands;

public class CreateProblemCommandHandler
{
    private readonly ISolutionToProblemAggregateRepository _repository;

    public CreateProblemCommandHandler(ISolutionToProblemAggregateRepository repository)
    {
        _repository = repository;
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

        SolutionToProblemData solutionToProblemData = new()
        {
            AggregateId = solutionToProblemAggregate.Id,
            UserId = command.UserId,
            Category = command.Category,
            Description = command.Description
        };

        await _repository.CreateProblem(solutionToProblemAggregate, solutionToProblemData);

        return solutionToProblemAggregate.Id.Value;
    }
}