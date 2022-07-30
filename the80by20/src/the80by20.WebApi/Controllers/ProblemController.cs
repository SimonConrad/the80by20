using MediatR;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.Core.SolutionToProblem.Commands;
using the80by20.App.Core.SolutionToProblem.ReadModel;

namespace the80by20.WebApi.Controllers
{
    [ApiController]
    [Route("solution-to-problem")]
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
            // TODO security
            // TODO fetch user and set in command

            var problemId = await _mediator.Send(createProblemCommand, token);

            return Ok(new { id = problemId });
        }

        [HttpPut("problem")]
        public async Task<IActionResult> Update(UpdatProblemCommand updateProblemCommand, CancellationToken token)
        {
            var problemId = await _mediator.Send(updateProblemCommand, token);

            return Ok(new { id = problemId });
        }

        [HttpGet("problems/{problemId}")]
        public async Task<IActionResult> Get(Guid problemId)
        {
            var res = await _solutionToProblemReadModelQueries.GetByProblemId(problemId);

            return Ok(res);
        }
    }
}