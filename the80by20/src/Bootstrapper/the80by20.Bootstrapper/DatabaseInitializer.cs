using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Modules.Masterdata.Infrastructure.EF;
using the80by20.Modules.Solution.Infrastructure.EF;
using the80by20.Modules.Users.Infrastructure.EF;
using the80by20.Shared.Abstractions.Time;

namespace the80by20.Bootstrapper;

// todo enebaled only in development environmetn in test, prod disabled and custom cli for running migratinons and seeding with data

// INFO not used, instead AppInitializer is used to migrate dbschema chnages and seed data
public class DatabaseInitializer : IHostedService
{
    // Service locator "anti-pattern" however needs to be there
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider,
        IClock clock,
        ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var solutionDbContext = scope.ServiceProvider.GetRequiredService<SolutionDbContext>();
        var masterDataDbCtxt = scope.ServiceProvider.GetRequiredService<MasterDataDbContext>();
        var usersDbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        // todo https://makolyte.com/ef-core-apply-migrations-programmatically/
        await solutionDbContext.Database.MigrateAsync(cancellationToken);
        await masterDataDbCtxt.Database.MigrateAsync(cancellationToken);
        await usersDbContext.Database.MigrateAsync(cancellationToken);

        if (await masterDataDbCtxt.Categories.AnyAsync(cancellationToken))
        {
            return;
        }

        var categories = new List<Category>
        {
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000001"), "typescript and angular"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000002"), "css and html"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000003"), "sql server"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000004"), "system analysis"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000005"), "buisness analysis"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000006"), "architecture"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000007"), "messaging"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000008"), "docker"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000009"), "craftsmanship"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000010"), "tests"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000011"), "ci / cd"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000012"), "deployment"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000013"), "azure"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000014"), "aws"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000015"), "monitoring"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000016"), "support"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000017"), ".net and c#")
        };

        await masterDataDbCtxt.Categories.AddRangeAsync(categories, cancellationToken);
        await masterDataDbCtxt.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}