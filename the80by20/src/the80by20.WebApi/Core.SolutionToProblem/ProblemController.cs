using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;
using the80by20.App.Core.SolutionToProblem.ReadModel;

namespace the80by20.WebApi.Core.SolutionToProblem
{
    [ApiController]
    [Route("solution-to-problem")]
    [Authorize]
    public class ProblemController : ControllerBase
    {
        private readonly ILogger<ProblemController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;

        public ProblemController(ILogger<ProblemController> logger, 
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
        }

        [HttpGet("categories-and-solution-types")]
        public async Task<IActionResult> GetCategoriesAndSolutionTypes()
        {
            var categories = await _solutionToProblemReadModelQueries.GetProblemsCategories();
            var solutionElementTypes =  _solutionToProblemReadModelQueries.GetSolutionElementTypes();

            return Ok(new{categories, solutionElementTypes});
        }

        [HttpPost("problem")]
        public async Task<IActionResult> Create(CreateProblemCommand createProblemCommand, CancellationToken token)
        {
            createProblemCommand = createProblemCommand with { UserId = Guid.Parse(User.Identity?.Name) };
            var problemId = await _mediator.Send(createProblemCommand, token);

            return CreatedAtAction(nameof(Get), new {problemId}, null);
        }

        [HttpPut("problem")]
        public async Task<IActionResult> Update(UpdateProblemCommand updateProblemCommand, CancellationToken token)
        {
            var problemId = await _mediator.Send(updateProblemCommand, token);

            return Ok(new { id = problemId });
        }

        [HttpGet("problems/{problemId}")]
        public async Task<IActionResult> Get(Guid problemId)
        {
            // todo do not return whole scope of this readmodel to users, caouse thers is solutions-elemnts there 
            var res = await _solutionToProblemReadModelQueries.GetByProblemId(problemId);

            return Ok(res);
        }
    }
}