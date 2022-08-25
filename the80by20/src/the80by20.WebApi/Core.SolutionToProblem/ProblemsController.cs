using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;
using the80by20.App.Core.SolutionToProblem.ReadModel;

namespace the80by20.WebApi.Core.SolutionToProblem
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
            var solutionElementTypes =  _solutionToProblemReadModelQueries.GetSolutionElementTypes();

            return Ok(new{categories, solutionElementTypes});
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProblemCommand createProblemCommand, CancellationToken token)
        {
            createProblemCommand = createProblemCommand with { UserId = Guid.Parse(User.Identity?.Name) };
            var problemId = await _mediator.Send(createProblemCommand, token);

            return CreatedAtAction(nameof(Get), new {problemId}, null);
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
    }
}