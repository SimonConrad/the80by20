using Bogus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.Core.SolutionToProblem.ReadModel;

namespace the80by20.WebApi.Core.SolutionToProblem
{
    [ApiController]
    [Route("solution-to-problem/[controller]")]
    [Authorize]
    public class SolutionsController : ControllerBase
    {
        private readonly ILogger<SolutionsController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;

        public SolutionsController(ILogger<SolutionsController> logger, 
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
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
    }
}