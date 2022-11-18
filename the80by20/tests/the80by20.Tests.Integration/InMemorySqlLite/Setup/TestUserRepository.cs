using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Tests.Integration.InMemorySqlLite.Setup;

internal sealed class TestUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public async Task<User> GetByIdAsync(UserId id)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Id == id);
    }

    public async Task<User> GetByEmailAsync(Email email)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Email == email);
    }

    public async Task<User> GetByUsernameAsync(Username username)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Username == username);
    }

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}