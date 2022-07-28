using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL.Administration;

public class AdministrationSchema
{
    public static void MapUsing(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
        });
    }
}