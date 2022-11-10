using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Users.App.Commands;

[CommandCqrs]
public record SignIn(string Email, string Password) : ICommand;