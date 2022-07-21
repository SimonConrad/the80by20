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
        
        public string Category { get; set; } // todo zrobić VO Kategoria

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

        // application logic - coordinates flow + cross cuttings:
        // wrap with db transaction - handler decorator or aspect oriented // todo like in grzybek's github
        // wrap with try catch logger
        public async Task Handle(CreateProblemCommand command)
        {
            // input validation logic
            // todo FluentValidator on command

            var solutionToProblemAggregate = SolutionToProblemAggregate.New(
                RequiredSolutionElementTypes.FromSolutionElements(command.SolutionElementTypes));

            SolutionToProblemData solutionToProblemData = new SolutionToProblemData()
            {
                AggregateId = solutionToProblemAggregate.Id,
                UserId = command.UserId,
                Category = command.Category,
                Description = command.Description
            };

            await _repository.CreateProblem(solutionToProblemAggregate, solutionToProblemData);

            // opcja reposne dla request
            // Created z id problemu
            // Responze 200
        }
    }
}
