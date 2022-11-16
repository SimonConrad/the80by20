using the80by20.Shared.Infrastucture.SqlServer;

namespace the80by20.Modules.Solution.Infrastructure.EF;


internal class SolutionUnitOfWork : SqlServerUnitOfWork<SolutionDbContext>, ISolutionUnitOfWork
{
    public SolutionUnitOfWork(SolutionDbContext dbContext) : base(dbContext)
    {
    }
}