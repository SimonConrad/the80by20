using MediatR;

namespace the80by20.App.Administration.Security.Commands;

public record SignInCommand(string Email, string Password) : IRequest;