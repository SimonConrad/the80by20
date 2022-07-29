using Core.App.Core.SolutionToProblem.Commands;
using Core.App.Core.SolutionToProblem.Commands.Handlers;
using Core.App.Core.SolutionToProblem.ReadModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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

            var solutionToProblemId = await _mediator.Send(createProblemCommand, token);

            return Ok(new { id = solutionToProblemId });
        }
    }
}