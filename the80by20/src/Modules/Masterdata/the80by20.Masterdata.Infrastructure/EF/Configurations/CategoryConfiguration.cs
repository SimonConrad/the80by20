using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.App.Entities;

namespace the80by20.Masterdata.Infrastructure.EF.Configurations;

public class CategoryConfiguration
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