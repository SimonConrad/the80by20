using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Infrastructure.DAL;

/// <summary>
/// A class used as a design-time factory for the DB Cotnext (https://go.microsoft.com/fwlink/?linkid=851728)
/// </summary>
public class CoreSqlServerDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CoreSqlServerDbContext>
{
    public CoreSqlServerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoreSqlServerDbContext>();
        optionsBuilder.UseSqlServer("Integrated Security=True;Initial Catalog=The80By20;Data Source=.");

        return new CoreSqlServerDbContext(optionsBuilder.Options);
    }
}