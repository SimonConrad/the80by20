using the80by20.App.Abstractions;

namespace the80by20.App.Security.Queries;
public record GetUsers : IQuery<IEnumerable<UserDto>>;

//public record GetUsersQueryMediatr : IRequest<IEnumerable<UserDto>>;