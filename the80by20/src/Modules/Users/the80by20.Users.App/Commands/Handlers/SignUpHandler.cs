using FluentValidation;
using the80by20.Modules.Users.App.Commands.Exceptions;
using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Time;

namespace the80by20.Modules.Users.App.Commands.Handlers;

[CommandHandlerCqrs]
internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUserRepository userRepository,
        IPasswordManager passwordManager,
        IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(SignUp command)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullName = new FullName(command.FullName);
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        var claims = command.Claims ?? new Dictionary<string, IEnumerable<string>>();

        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = _passwordManager.HashPassword(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, _clock.CurrentDate(), claims, isActive: true);
        await _userRepository.AddAsync(user);
    }
}

// INFO input validation logic, do not check db there it's reposoibility of application logic
public sealed class SignUpInputValidator : AbstractValidator<SignUp>
{
    public SignUpInputValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(2)
            .WithMessage("Username min length is 2");
    }
}