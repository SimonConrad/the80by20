using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.SharedKernel;
using Core.Domain.SolutionToProblem;

namespace Core.App.SolutionToProblem
{
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

        public CreateProblemCommandHandler(ISolutionToProblemAggregateRepository repository)
        {
            _repository = repository;
        }

        // INFO application logic - coordinates flow + cross cuttings:
        // wrap with db transaction - handler decorator or aspect oriented
        // wrap with try catch logger
        public async Task Handle(CreateProblemCommand command)
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

            // INFO calling controler action can response to request using Created with id or Ok, or Bad Request
        }
    }
}
