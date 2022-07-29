using MediatR;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.Core.SolutionToProblem.ReadModel;

namespace the80by20.WebApi.Controllers
{
    [ApiController]
    [Route("solution-to-problem")]
    public class SolutionToProblemController : ControllerBase
    {
        private readonly ILogger<SolutionToProblemController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;

        public SolutionToProblemController(ILogger<SolutionToProblemController> logger, 
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
        }

        [HttpGet("solutions-to-problems/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _solutionToProblemReadModelQueries.Get(id);

            return Ok(res);
        }
    }
}