using the80by20.App.Abstractions;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.DAL;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly CoreDbContext _coreDbContext;

    public EfUnitOfWork(CoreDbContext coreDbContext)
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