using the80by20.App.Administration.Security.User;

namespace the80by20.App.Administration.Security;

public interface IUserRepository
{
    Task<User.User> GetByIdAsync(UserId id);
    Task<User.User> GetByEmailAsync(Email email);
    Task<User.User> GetByUsernameAsync(Username username);
    Task AddAsync(User.User user);
}