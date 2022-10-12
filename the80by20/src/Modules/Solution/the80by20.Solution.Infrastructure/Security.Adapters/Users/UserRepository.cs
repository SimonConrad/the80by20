using Microsoft.EntityFrameworkCore;
using the80by20.Solution.App.Security.Ports;
using the80by20.Solution.Domain.Security.UserEntity;
using the80by20.Solution.Infrastructure.EF;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Users
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly SolutionDbContext _dbContext;

        public UserRepository(SolutionDbContext dbContext)
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