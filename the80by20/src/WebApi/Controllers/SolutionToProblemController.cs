using Core.App.SolutionToProblem;
using Core.App.SolutionToProblem.Commands;
using Core.App.SolutionToProblem.Reads;
using Microsoft.AspNetCore.Mvc;
using WebApi.SolutionToProblemReads;

namespace WebApi.Controllers
{
    // INFO Use helper methods like Created with id or Ok, or Bad Request
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionToProblemController : ControllerBase
    {
        private readonly ILogger<SolutionToProblemController> _logger;
        private readonly CreateProblemCommandHandler _createProblemCommandHandler;
        private readonly ISolutionToProblemReader _solutionToProblemReader;

        public SolutionToProblemController(ILogger<SolutionToProblemController> logger, 
            CreateProblemCommandHandler createProblemCommandHandler,
            ISolutionToProblemReader solutionToProblemReader)
        {
            _logger = logger;

            _createProblemCommandHandler = createProblemCommandHandler;
            _solutionToProblemReader = solutionToProblemReader;
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
            var res = await _solutionToProblemReader.Get(id);

            return Ok(res);
        }
    }
}