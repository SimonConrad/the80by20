using MediatR;

namespace the80by20.App.Security.Queries;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;