using the80by20.App.Security.Queries;
using the80by20.Domain.Security.UserEntity;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new()
        {
            Id = entity.Id,
            Username = entity.Username,
            FullName = entity.FullName
        };
}