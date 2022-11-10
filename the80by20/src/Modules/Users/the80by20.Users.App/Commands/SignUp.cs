using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Users.App.Commands;

[CommandCqrs]
public record SignUp(Guid UserId,
    string Email,
    string Username,
    string Password,
    string FullName,
    string Role,
    Dictionary<string, IEnumerable<string>> Claims) : ICommand;
