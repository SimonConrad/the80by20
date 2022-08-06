using MediatR;
using the80by20.App.Abstractions;

namespace the80by20.App.Security.Commands;
public record SignIn(string Email, string Password) : ICommand;

//public record SignInCommand(string Email, string Password) : IRequest;