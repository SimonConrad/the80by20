using Core.App.Administration.Security.User;

namespace Core.App.Administration.Security;

public interface IUserRepository
{
    Task<User.User> GetByIdAsync(UserId id);
    Task<User.User> GetByEmailAsync(Email email);
    Task<User.User> GetByUsernameAsync(Username username);
    Task AddAsync(User.User user);
}