using Core.App.Administration.Security;
using Core.App.Administration.Security.User;
using Core.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Administration.Security;

public sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(CoreDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);
}