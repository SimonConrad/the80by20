using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using the80by20.Modules.Users.App.Commands;
using the80by20.Modules.Users.App.Queries;
using the80by20.Shared.Abstractions.Auth;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Shared.Infrastucture;

namespace the80by20.Modules.Users.Api.Controllers;

[Authorize(Policy = Policy)]
internal class UsersController : BaseController
{
    private const string Policy = "users";

    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly ITokenStorage _tokenStorage;
    private readonly AppOptions _appOptions;
    private readonly IOptionsMonitor<AppOptions> _appOptionsMonitor;

    public UsersController(IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
        IQueryHandler<GetUser, UserDto> getUserHandler,
        ICommandHandler<SignIn> signInHandler,
        ICommandHandler<SignUp> signUpHandler,
        ITokenStorage tokenStorage,
        IOptions<AppOptions> appOptions,
        IOptionsMonitor<AppOptions> appOptionsMonitor)
    {
        _getUsersHandler = getUsersHandler;
        _getUserHandler = getUserHandler;
        _signInHandler = signInHandler;
        _signUpHandler = signUpHandler;
        _tokenStorage = tokenStorage;

        _appOptions = appOptions.Value;
        _appOptionsMonitor = appOptionsMonitor; // reflect values in json without restarting app
    }


    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() }; // INFO creating record by copying it and adding UserId
        await _signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { command.UserId }, null);
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> Post([FromBody] SignIn command)
    {
        // todo add claims before encoding jwt token like username and role
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        var user = await _getUserHandler.HandleAsync(new GetUser() { UserId = userId });

        return user;
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
       => Ok(await _getUsersHandler.HandleAsync(new GetUsers()));

    
    [Authorize(Policy = "is-admin")] // INFO module level policy 'users' and action level policy 'is-admin' needed 
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // todo check what it gives - swagger?
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser() { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
}