using Bogus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using the80by20.Solution.App.Commands.Problem;
using the80by20.Solution.App.ReadModel;

namespace the80by20.Solution.Api.Controllers
{
    [ApiController]
    [Route("solution-to-problem/[controller]")]
    [Authorize]
    public class ProblemsController : ControllerBase
    {
        private readonly ILogger<ProblemsController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;

        public ProblemsController(ILogger<ProblemsController> logger,
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
        }

        [HttpGet("categories-and-solution-types")]
        public async Task<ActionResult> GetCategoriesAndSolutionTypes()
        {
            var categories = await _solutionToProblemReadModelQueries.GetProblemsCategories();
            var solutionElementTypes = _solutionToProblemReadModelQueries.GetSolutionElementTypes();

            return Ok(new { categories, solutionElementTypes });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProblemCommand createProblemCommand, CancellationToken token)
        {
            createProblemCommand = createProblemCommand with { UserId = Guid.Parse(User.Identity?.Name) };
            var problemId = await _mediator.Send(createProblemCommand, token);

            return CreatedAtAction(nameof(Get), new { problemId }, null);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProblemCommand updateProblemCommand, CancellationToken token)
        {
            var problemId = await _mediator.Send(updateProblemCommand, token);

            return Ok(new { id = problemId });

            // info more reststyle will be to:
            // - add id as action parameter with attribute fromroute
            // - return baadrequest if not valid and return notcontent if valid
            // - rename action name to put
        }

        [HttpGet("{problemId:guid}")]
        public async Task<ActionResult<SolutionToProblemReadModel>> Get(Guid problemId)
        {
            // todo do not return whole scope of this readmodel to users, caouse thers is solutions-elemnts there 
            var res = await _solutionToProblemReadModelQueries.GetByProblemId(problemId);

            return Ok(res);
        }


        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<SolutionToProblemReadModel[]>> Get()
        {
            // TODO dedicated dto, to not leak fragile properties like price etc - dedicated readmodel scope
            var faker = new Faker<SolutionToProblemReadModel>()
                .RuleFor(d => d.Id, d => Guid.NewGuid())
                .RuleFor(d => d.UserId, d => Guid.Parse(User.Identity?.Name));

            var res = faker.Generate(3);

            res[0].RequiredSolutionTypes = "PocInCode; PlanOfImplmentingChangeInCode";
            res[0].Description = "refactor to cqrs instead of not cohesive services, srp against separate user use case";
            res[0].CategoryId = new Guid("00000000-0000-0000-0000-000000000006");
            res[0].IsConfirmed = false;
            res[0].IsRejected = false;

            res[1].RequiredSolutionTypes = "TheoryOfConceptWithExample";
            res[1].Description = "refactor anemic entity + service into ddd object oriented model (entities with behaviors, aggreagtes, value objects)";
            res[1].CategoryId = new Guid("00000000-0000-0000-0000-000000000006");
            res[1].IsConfirmed = false;
            res[1].IsRejected = true;

            res[2].RequiredSolutionTypes = "RoiAnalysis";
            res[2].Description = "introduce integration tests and unit test into existing code)";
            res[2].CategoryId = new Guid("00000000-0000-0000-0000-000000000010");
            res[2].IsConfirmed = true;
            res[2].IsRejected = false;

            var resFromdb = await _solutionToProblemReadModelQueries.GetByUserId(Guid.Parse(User.Identity?.Name));

            res.AddRange(resFromdb);

            return Ok(res);
        }
    }
}