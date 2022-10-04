﻿using the80by20.App.Abstractions;
using the80by20.App.Security.Commands.Exceptions;
using the80by20.App.Security.Ports;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Security.Commands.Handlers;

[CommandHandlerCqrs]
internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;

    public SignInHandler(IUserRepository userRepository, 
        IAuthenticator authenticator, 
        IPasswordManager passwordManager,
        ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }
    
    // todo version with cancelationtoken
    public async Task HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role, user.Username);
        _tokenStorage.Set(jwt);
    }
}

//public class SignInCommandHandler : IRequestHandler<SignInCommand>