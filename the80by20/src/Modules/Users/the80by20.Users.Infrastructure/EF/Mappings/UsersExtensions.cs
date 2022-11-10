using the80by20.Modules.Users.App.DTO;
using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Modules.Users.Infrastructure.EF.Mappings
{
    public static class UsersExtensions
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