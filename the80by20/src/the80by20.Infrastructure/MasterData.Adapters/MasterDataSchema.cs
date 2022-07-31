using Microsoft.EntityFrameworkCore;
using the80by20.App.MasterData.CategoryCrud;

namespace the80by20.Infrastructure.MasterData.Adapters;

public class MasterDataSchema
{
    public static void MapUsing(ModelBuilder builder)
    {
        builder.Entity<Category>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
        });
    }
}