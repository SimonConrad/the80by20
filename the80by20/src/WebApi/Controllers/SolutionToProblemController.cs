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
        private readonly ISolutionToProblemReadModelRepository _solutionToProblemReadModelRepository;
        private readonly IMediator _mediator;

        public SolutionToProblemController(ILogger<SolutionToProblemController> logger, 
            CreateProblemCommandHandler createProblemCommandHandler,
            ISolutionToProblemReadModelRepository solutionToProblemReadModelRepository,
            IMediator mediator)
        {
            _logger = logger;

            _createProblemCommandHandler = createProblemCommandHandler;
            _solutionToProblemReadModelRepository = solutionToProblemReadModelRepository;
            _mediator = mediator;
        }

        [HttpGet("/categoriesandsolutiontypes")]
        public async Task<IActionResult> GetCategoriesAndSolutionTypes()
        {
            var categories = await _solutionToProblemReadModelRepository.GetProblemsCategories();
            var solutionTypes =  _solutionToProblemReadModelRepository.GetSolutionElementTypes();

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
            var res = await _solutionToProblemReadModelRepository.Get(id);

            return Ok(res);
        }
    }
}