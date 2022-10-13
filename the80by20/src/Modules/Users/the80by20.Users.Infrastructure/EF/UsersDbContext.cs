

using Microsoft.EntityFrameworkCore;
using the80by20.Users.Domain.UserEntity;
using the80by20.Users.Infrastructure.EF.Configurations;

namespace the80by20.Users.Infrastructure.EF
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            UserConfiguration.MapUsing(modelBuilder);
        }
    }
}
