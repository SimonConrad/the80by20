﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using the80by20.App.Abstractions;
using the80by20.App.Security.Commands;
using the80by20.App.Security.Ports;
using the80by20.App.Security.Queries;
using the80by20.Infrastructure;

namespace the80by20.WebApi.Security;

// todo is convention userscontroller vs usercontroller

[ApiController] // info bacouse of inheriting from ControllerBase and marking controllre as [ApiController] attributes: FromRoute, FromQuery, FromBody can be removed
[Route("security/[controller]")]
public class UsersController  : ControllerBase
{
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

    [Authorize(Policy = "is-admin")]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // todo check what it gives - swagger?
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser(userId));
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        var user = await _getUserHandler.HandleAsync(new GetUser(userId));

        return user;
    }

    [HttpGet]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = "is-admin")] // todo apply other staff from asp.net identity like requirements etc
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(new GetUsers()));

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

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post([FromBody] SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }
}