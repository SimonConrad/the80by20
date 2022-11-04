using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Domain.UserEntity;


namespace the80by20.Modules.Users.Infrastructure.EF.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public Task<User> GetByIdAsync(UserId id)
            => _users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetByEmailAsync(Email email)
            => _users.SingleOrDefaultAsync(x => x.Email == email);

        public Task<User> GetByUsernameAsync(Username username)
            => _users.SingleOrDefaultAsync(x => x.Username == username);

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}