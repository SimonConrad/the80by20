using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common;

public  static class EfCoreExtensions
{
    public static void MapBaseEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : class// where T : BaseEntity
    {
        builder.Property("Version").IsRowVersion();
    }
}