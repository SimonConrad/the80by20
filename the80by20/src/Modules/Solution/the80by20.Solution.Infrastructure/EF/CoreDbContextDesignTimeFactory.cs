using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace the80by20.Solution.Infrastructure.EF
{
    /// <summary>
    /// A class used as a design-time factory for the DB Cotnext (https://go.microsoft.com/fwlink/?linkid=851728)
    /// </summary>
    public class CoreDbContextDesignTimeFactory : IDesignTimeDbContextFactory<SolutionDbContext>
    {
        public SolutionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SolutionDbContext>();
            optionsBuilder.UseSqlServer("Integrated Security=True;Initial Catalog=The80By20;Data Source=.");

            return new SolutionDbContext(optionsBuilder.Options);
        }
    }
}