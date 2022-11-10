using Bogus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using the80by20.Modules.Solution.App.Events.External;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Modules.Solution.Api.Controllers
{

    [Authorize(Policy = Policy)]
    internal class SolutionsController : BaseController
    {
        private const string Policy = "solution";

        private readonly ILogger<SolutionsController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;
        private readonly IMessageBroker _messageBroker;

        public SolutionsController(ILogger<SolutionsController> logger,
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator,
            IMessageBroker messageBroker)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
            _messageBroker = messageBroker;
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

        [HttpPost("FinishSolutionMocked")]
        public async Task<ActionResult> FinishSolutionMocked()
        {
            // todo
            // move to FinishSolutionCommandHandler (API layer or DAL Layer in the repo after save-changes)
            // should go first to FinishSolutionCommandHandler and after successfully command handled in this handler call below

            await _messageBroker.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));
            return Ok();
        }
    }
}