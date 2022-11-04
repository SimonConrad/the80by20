using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Modules.Users.App.Ports;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}