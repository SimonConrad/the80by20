using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace the80by20.Infrastructure.DAL.Misc;

public  static class EfCoreExtensions
{
    public static void MapTechnicalProperties<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.Property("Version").IsRowVersion();

        // TODO add audit
    }
}