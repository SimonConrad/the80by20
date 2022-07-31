using MediatR;

namespace the80by20.App.Security.Queries;

public record GetUserQuery(Guid UserId) : IRequest<UserDto>;