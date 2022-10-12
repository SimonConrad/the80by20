using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace the80by20.Solution.Infrastructure.DAL.DbContext
{
    /// <summary>
    /// A class used as a design-time factory for the DB Cotnext (https://go.microsoft.com/fwlink/?linkid=851728)
    /// </summary>
    public class CoreDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseSqlServer("Integrated Security=True;Initial Catalog=The80By20;Data Source=.");

            return new CoreDbContext(optionsBuilder.Options);
        }
    }
}