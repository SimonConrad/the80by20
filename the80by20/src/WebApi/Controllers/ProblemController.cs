using Core.App.SolutionToProblem;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly ILogger<ProblemController> _logger;
        private readonly CreateProblemCommandHandler _createProblemCommandHandler;
        private readonly ProblemReader _reader;

        public ProblemController(ILogger<ProblemController> logger, 
            CreateProblemCommandHandler createProblemCommandHandler,
            ProblemReader reader)
        {
            _logger = logger;
            _createProblemCommandHandler = createProblemCommandHandler;
            _reader = reader;
        }

        [HttpPost("/problem")]
        public async Task<IActionResult> Create(CreateProblemDto createProblemDto)
        {
            // TODO do input validation logic using flunet validator
            // TODO security, fetch user and set in command

            await _createProblemCommandHandler.Handle(createProblemDto.MapToCommand());

            return Ok();
        }

        [HttpGet("/problems")]
        public async Task<IActionResult> Get()
        {
            await _reader.Get();

            return Ok();
        }
    }
}