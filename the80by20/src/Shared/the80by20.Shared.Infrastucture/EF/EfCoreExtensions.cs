using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace the80by20.Shared.Infrastucture.EF;

public static class EfCoreExtensions
{
    public static void MapTechnicalProperties<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.Property("Version").IsRowVersion(); //  // TODO  do in one way - like in aggregates

        // TODO add audit
    }
}