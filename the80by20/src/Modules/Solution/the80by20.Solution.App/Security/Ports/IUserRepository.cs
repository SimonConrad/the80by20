using the80by20.Solution.Domain.Security.UserEntity;

namespace the80by20.Solution.App.Security.Ports;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}