using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Shared.Infrastucture.Services;

namespace the80by20.Modules.Masterdata.Infrastructure.EF
{
    public class MasterDataSeeder : IDataSeeder
    {
        public async Task Seed(IServiceScope scope, CancellationToken cancellationToken)
        {
            var masterDataDbCtxt = scope.ServiceProvider.GetRequiredService<MasterDataDbContext>();

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
    }
}
