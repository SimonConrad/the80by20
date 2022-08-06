using the80by20.App.Abstractions;

namespace the80by20.App.Security.Queries;

public record GetUser(Guid UserId) : IQuery<UserDto>;

//public record GetUserQueryMediatr(Guid UserId) : IRequest<UserDto>;