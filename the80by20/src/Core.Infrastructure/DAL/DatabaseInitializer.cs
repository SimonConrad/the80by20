using Core.App.Administration;
using Core.Domain.SharedKernel;
using Core.Infrastructure.DAL.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.DAL;

public class DatabaseInitializer : IHostedService
{
    // Service locator "anti-pattern" however needs to be there
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;
    private readonly IOptions<DatabaseOptions> _options;

    public DatabaseInitializer(IServiceProvider serviceProvider, IClock clock, IOptions<DatabaseOptions> options)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _options = options;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CoreDbContext>();

        // todo https://makolyte.com/ef-core-apply-migrations-programmatically/
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Category.AnyAsync(cancellationToken))
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
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000016"), "support")
        };

        await dbContext.Category.AddRangeAsync(categories, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}