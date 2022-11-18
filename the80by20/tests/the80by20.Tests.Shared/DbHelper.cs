using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace the80by20.Tests.Shared
{
    public static class DbHelper
    {
        private static readonly IConfiguration Configuration = OptionsHelper.GetConfigurationRoot();

        public static DbContextOptions<T> GetOptions<T>() where T : DbContext
            => new DbContextOptionsBuilder<T>()
                .UseSqlServer(Configuration["dataBase:connectionString"])
                .EnableSensitiveDataLogging()
                .Options;
    }
}