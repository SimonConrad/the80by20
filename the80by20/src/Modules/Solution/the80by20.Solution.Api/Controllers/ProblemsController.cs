using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using the80by20.Modules.Solution.App.Problem.Commands;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Contexts;

namespace the80by20.Modules.Solution.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class ProblemsController : BaseController
    {
        private const string Policy = "solution";

        private readonly ILogger<ProblemsController> _logger;
        private readonly ISolutionToProblemReadModelQueries _solutionToProblemReadModelQueries;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IContext _context;

        public ProblemsController(ILogger<ProblemsController> logger,
            ISolutionToProblemReadModelQueries solutionToProblemReadModelQueries,
            ICommandDispatcher commandDispatcher,
            IContext context)
        {
            _logger = logger;

            _solutionToProblemReadModelQueries = solutionToProblemReadModelQueries;
            _commandDispatcher = commandDispatcher;
            _context = context;
        }

        [HttpGet("categories-and-solution-types")]
        public async Task<ActionResult> GetCategoriesAndSolutionTypes()
        {
            var categories = await _solutionToProblemReadModelQueries.GetProblemsCategories();
            var solutionElementTypes = _solutionToProblemReadModelQueries.GetSolutionElementTypes();

            return Ok(new { categories, solutionElementTypes });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RequestProblemCommand createProblemCommand, CancellationToken token)
        {
            createProblemCommand = createProblemCommand with
            {
                UserId = _context.Identity.Id
            };
            await _commandDispatcher.SendAsync(createProblemCommand);

            return CreatedAtAction(nameof(Get), new { createProblemCommand.Id }, null);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProblemCommand updateProblemCommand, CancellationToken token)
        {
           await _commandDispatcher.SendAsync(updateProblemCommand);

            return Ok();

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


        [HttpGet()]
        public async Task<ActionResult<SolutionToProblemReadModel[]>> Get()
        {
            // TODO dedicated dto, to not leak fragile properties like price etc - dedicated readmodel scope
            var faker = new Faker<SolutionToProblemReadModel>()
                .RuleFor(d => d.Id, d => Guid.NewGuid())
                .RuleFor(d => d.UserId, d => _context.Identity.Id);

            var res = faker.Generate(4);

            res[0].RequiredSolutionTypes = "PocInCode; PlanOfImplmentingChangeInCode";
            res[0].Description = "refactor to cqrs instead of not cohesive services, srp against separate user use case";
            res[0].CategoryId = new Guid("00000000-0000-0000-0000-000000000006");
            res[0].IsConfirmed = false;
            res[0].IsRejected = false;

            res[1].RequiredSolutionTypes = "TheoryOfConceptWithExample";
            res[1].Description = "refactor anemic entity + service into ddd object oriented model (entities with behaviors, aggreagtes, value objects)";
            res[1].CategoryId = new Guid("00000000-0000-0000-0000-000000000006");
            res[1].IsConfirmed = false;
            res[1].IsRejected = true;

            res[2].RequiredSolutionTypes = "RoiAnalysis";
            res[2].Description = "introduce integration tests and unit test into existing code)";
            res[2].CategoryId = new Guid("00000000-0000-0000-0000-000000000010");
            res[2].IsConfirmed = true;
            res[2].IsRejected = false;

            res[3].RequiredSolutionTypes = "TheoryOfConceptWithExample; PocInCode";
            res[3].Description = "modelling process with event storming; and implementing this model it into application and domain layers)";
            res[3].CategoryId = new Guid("00000000-0000-0000-0000-000000000010");
            res[3].IsConfirmed = true;
            res[3].IsRejected = false;

            //modelling process with event storming; and implementing this model it into application and domain layers

            var resFromdb = await _solutionToProblemReadModelQueries.GetByUserId(Guid.Parse(User.Identity?.Name));

            res.AddRange(resFromdb);

            return Ok(res);
        }
    }
}