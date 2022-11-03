using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Domain.UserEntity;


namespace the80by20.Modules.Users.Infrastructure.EF.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User> GetByIdAsync(UserId id)
            => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetByEmailAsync(Email email)
            => _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

        public Task<User> GetByUsernameAsync(Username username)
            => _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}