using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using the80by20.Modules.Users.App.Commands;
using the80by20.Modules.Users.App.DTO;
using the80by20.Modules.Users.App.Queries;
using the80by20.Shared.Abstractions.Auth;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Contexts;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Shared.Infrastucture;

namespace the80by20.Modules.Users.Api.Controllers;

[Authorize(Policy = Policy)]
internal class UsersController : BaseController
{
    private const string Policy = "users";

    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;


    private readonly ITokenStorage _tokenStorage;
    private readonly AppOptions _appOptions;
    private readonly IOptionsMonitor<AppOptions> _appOptionsMonitor;
    private readonly IContext _context;

    public UsersController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ITokenStorage tokenStorage,
        IOptions<AppOptions> appOptions,
        IOptionsMonitor<AppOptions> appOptionsMonitor,
        IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _tokenStorage = tokenStorage;
        _appOptions = appOptions.Value;
        _appOptionsMonitor = appOptionsMonitor; // INFO reflect values in json without restarting app
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() }; // INFO creating record by copying it and adding UserId
        await _commandDispatcher.SendAsync(command);

        return CreatedAtAction(nameof(Get), new { command.UserId }, null);
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> Post([FromBody] SignIn command)
    {
        await _commandDispatcher.SendAsync(command);

        var jwt = _tokenStorage.Get();
        return jwt;
    }

    // TODO
    // sign-out

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(_context.Identity.Id.ToString()))
        {
            return NotFound(); // TODO refactor to use OkOrNotFound from base controller
        }

        var user = await _queryDispatcher.QueryAsync(new GetUser() { UserId = _context.Identity.Id });

        return user;
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
       //=> Ok(await _getUsersHandler.HandleAsync(new GetUsers()));
       => Ok(await _queryDispatcher.QueryAsync(new GetUsers()));

    
    [Authorize(Policy = "is-admin")] // INFO module level policy 'users' and action level policy 'is-admin' needed 
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // todo check what it gives - swagger?
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId)
    {
        var user = await _queryDispatcher.QueryAsync(new GetUser() { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
}