using the80by20.Modules.Users.App.Commands.Exceptions;
using the80by20.Modules.Users.App.Ports;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Auth;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Users.App.Commands.Handlers;

[CommandHandlerCqrs]
internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;

    public SignInHandler(IUserRepository userRepository,
        IAuthManager authenticator,
        IPasswordManager passwordManager,
        ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }

    // TODO
    // version with cancelation-token
    public async Task HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.VerifyHashedPassword(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        if (!user.IsActive)
        {
            throw new UserNotActiveException(user.Id);
        }

        // INFO
        // https://jwt.io/
        var jwt = _authenticator.CreateToken(user.Id.Value.ToString(), user.Role, claims: user.Claims, email: user.Email);
        jwt.Email = user.Email;

        _tokenStorage.Set(jwt);
    }
}