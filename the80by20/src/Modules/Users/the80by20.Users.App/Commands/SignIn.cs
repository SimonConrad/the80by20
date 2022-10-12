﻿using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Users.App.Commands;

[CommandCqrs]
public record SignIn(string Email, string Password) : ICommand;

//public record SignInCommand(string Email, string Password) : IRequest;