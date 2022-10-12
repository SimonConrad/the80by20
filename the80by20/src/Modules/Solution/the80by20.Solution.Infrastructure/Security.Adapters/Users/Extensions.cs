using the80by20.Solution.Domain.Security.UserEntity;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Users
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