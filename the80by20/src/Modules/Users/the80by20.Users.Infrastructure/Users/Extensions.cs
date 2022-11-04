using the80by20.Modules.Users.App.Queries;
using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Modules.Users.Infrastructure.Users
{
    public static class Extensions
    {
        public static UserDto AsDto(this User entity)
            => new()
            {
                Id = entity.Id,
                Email = entity.Email,
                Username = entity.Username,
                FullName = entity.FullName,
                Role = entity.Role,
                IsActive = entity.IsActive,
                Claims = entity.Claims,
                CreatedAt = entity.CreatedAt
            };
    }
}