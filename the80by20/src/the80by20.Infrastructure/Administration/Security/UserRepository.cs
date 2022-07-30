using Microsoft.EntityFrameworkCore;
using the80by20.App.Administration.Security;
using the80by20.App.Administration.Security.User;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Administration.Security;

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