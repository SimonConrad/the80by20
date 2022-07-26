using Core.App.SolutionToProblem;
using Core.App.SolutionToProblem.Commands;
using Core.App.SolutionToProblem.ReadModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // INFO Use helper methods like Created with id or Ok, or Bad Request
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionToProblemController : ControllerBase
    {
        private readonly ILogger<SolutionToProblemController> _logger;
        private readonly CreateProblemCommandHandler _createProblemCommandHandler;
        private readonly ISolutionToProblemReadModelRepository _solutionToProblemReadModelRepository;

        public SolutionToProblemController(ILogger<SolutionToProblemController> logger, 
            CreateProblemCommandHandler createProblemCommandHandler,
            ISolutionToProblemReadModelRepository solutionToProblemReadModelRepository)
        {
            _logger = logger;

            _createProblemCommandHandler = createProblemCommandHandler;
            _solutionToProblemReadModelRepository = solutionToProblemReadModelRepository;
        }

        [HttpPost("/problem")]
        public async Task<IActionResult> Create(CreateProblemCommand createProblemCommand)
        {
            // TODO security
            // TODO fetch user and set in command

            var solutionToProblemId = await _createProblemCommandHandler.Handle(createProblemCommand);

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