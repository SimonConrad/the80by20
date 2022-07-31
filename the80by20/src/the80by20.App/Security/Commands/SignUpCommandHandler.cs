﻿using MediatR;
using the80by20.App.Security.Commands.Exceptions;
using the80by20.App.Security.Ports;
using the80by20.Domain.Security.UserEntity;
using the80by20.Domain.SharedKernel;

namespace the80by20.App.Security.Commands;

internal sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpCommandHandler(IUserRepository userRepository, 
        IPasswordManager passwordManager, 
        IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }
    public async Task<Unit> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullName = new FullName(command.FullName);
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = _passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, _clock.Current());
        await _userRepository.AddAsync(user);
        
        return Unit.Value;
    }
}