using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Solution.Infrastructure.EF;

namespace the80by20.Solution.Infrastructure.DAL
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly SolutionDbContext _coreDbContext;

        public EfUnitOfWork(SolutionDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _coreDbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await _coreDbContext.SaveChangesAsync();
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