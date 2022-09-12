using the80by20.App.Abstractions;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Security.Commands;

[CommandCqrs]
public record SignIn(string Email, string Password) : ICommand;

//public record SignInCommand(string Email, string Password) : IRequest;