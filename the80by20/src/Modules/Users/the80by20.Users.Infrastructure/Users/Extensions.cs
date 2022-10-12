using the80by20.Users.App.Queries;
using the80by20.Users.Domain.UserEntity;

namespace the80by20.Users.Infrastructure.Users
{
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
}