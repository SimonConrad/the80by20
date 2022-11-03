using the80by20.Shared.Abstractions.Dal;

namespace the80by20.Modules.Users.Infrastructure.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _dbContext;

        public EfUnitOfWork(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}