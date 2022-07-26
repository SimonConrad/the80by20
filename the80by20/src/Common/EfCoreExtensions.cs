using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common;

public  static class EfCoreExtensions
{
    public static void MapTechnicalProperties<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.Property("Version").IsRowVersion();

        // TODO add audit
    }
}