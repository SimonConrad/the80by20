using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace the80by20.Users.Infrastructure.EF
{
    /// <summary>
    /// A class used as a design-time factory for the DB Cotnext (https://go.microsoft.com/fwlink/?linkid=851728)
    /// </summary>
    public class UsersDbContextDesignTimeFactory : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public UsersDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
            // todo do this connection string as secret, like in AzureServiceBusTests.cs
            optionsBuilder.UseSqlServer("Integrated Security=True;Initial Catalog=The80By20;Data Source=.");

            return new UsersDbContext(optionsBuilder.Options);
        }
    }
}