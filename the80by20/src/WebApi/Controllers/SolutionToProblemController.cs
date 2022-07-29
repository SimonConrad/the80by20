using Core.App.SolutionToProblem.Commands;
using Core.App.SolutionToProblem.Commands.Handlers;
using Core.App.SolutionToProblem.ReadModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionToProblemController : ControllerBase
    {
        private readonly ILogger<SolutionToProblemController> _logger;
        private readonly CreateProblemCommandHandler _createProblemCommandHandler;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly IMediator _mediator;

        public SolutionToProblemController(ILogger<SolutionToProblemController> logger, 
            CreateProblemCommandHandler createProblemCommandHandler,
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            IMediator mediator)
        {
            _logger = logger;

            _createProblemCommandHandler = createProblemCommandHandler;
            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _mediator = mediator;
        }

        [HttpGet("/categoriesandsolutiontypes")]
        public async Task<IActionResult> GetCategoriesAndSolutionTypes()
        {
            var categories = await _solutionToProblemReadModelQueries.GetProblemsCategories();
            var solutionTypes =  _solutionToProblemReadModelQueries.GetSolutionElementTypes();

            return Ok(new{categories, solutionTypes});
        }

        [HttpPost("/problem")]
        public async Task<IActionResult> Create(CreateProblemCommand createProblemCommand, CancellationToken token)
        {
            // TODO security
            // TODO fetch user and set in command

            var solutionToProblemId = await _mediator.Send(createProblemCommand, token);

            return Ok(new { id = solutionToProblemId });
        }

        [HttpGet("/solutionstoproblems/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _solutionToProblemReadModelQueries.Get(id);

            return Ok(res);
        }
    }
}