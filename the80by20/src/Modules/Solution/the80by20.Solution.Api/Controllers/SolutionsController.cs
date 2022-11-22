using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.App.Solution.Commands;
using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Solution.Api.Controllers
{

    [Authorize(Policy = Policy)]
    internal class SolutionsController : BaseController
    {
        private const string Policy = "solution";

        private readonly ILogger<SolutionsController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMessageBroker _messageBroker;
        private readonly ICommandDispatcher _commandDispatcher;

        public SolutionsController(ILogger<SolutionsController> logger,
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMessageBroker messageBroker,
            ICommandDispatcher commandDispatcher)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _messageBroker = messageBroker;
            _commandDispatcher = commandDispatcher;
        }

        // todo swagger attributes and proper methods like notfound etc
        [HttpGet("{solutionId:guid}")]
        public async Task<ActionResult<SolutionToProblemReadModel>> Get(Guid solutionId)
        {
            var res = await _solutionToProblemReadModelQueries.GetBySolutionId(solutionId);

            return Ok(res);
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<SolutionToProblemReadModel[]>> Get()
        {
            var faker = new Faker<SolutionToProblemReadModel>()
                .RuleFor(d => d.Id, d => Guid.NewGuid())
                .RuleFor(d => d.UserId, d => Guid.NewGuid());

            var res = faker.Generate(10);

            return Ok(res);
        }

        [HttpPost("FinishSolutionMocked/{solutionId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> FinishSolutionMocked(Guid solutionId)
        {
            // todo
            // move to FinishSolutionCommandHandler (API layer or DAL Layer in the repo after save-changes)
            // should go first to FinishSolutionCommandHandler and after successfully command handled in this handler call below
            //await  _commandDispatcher.SendAsync(new FinishSolutionCommand(solutionId));

            await _messageBroker.PublishAsync(new SolutionFinished(Guid.NewGuid()));


            return Ok();
        }
    }
}