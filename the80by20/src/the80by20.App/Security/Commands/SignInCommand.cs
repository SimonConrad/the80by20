using MediatR;

namespace the80by20.App.Security.Commands;

public record SignInCommand(string Email, string Password) : IRequest;