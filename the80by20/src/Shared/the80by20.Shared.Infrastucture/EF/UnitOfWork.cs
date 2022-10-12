using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.AppLayer;

namespace the80by20.Shared.Infrastucture.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public EfUnitOfWork(DbContext dbContext)
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