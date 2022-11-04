using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Modules.Users.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                    x => x.Value,
                    x => new UserId(x));

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                .HasConversion(
                    x => x.Value,
                    x => new Email(x))
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username)
                .HasConversion(
                    x => x.Value,
                    x => new Username(x))
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .HasConversion(
                    x => x.Value,
                    x => new Password(x))
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FullName)
                .HasConversion(
                    x => x.Value,
                    x => new FullName(x))
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Role)
                .HasConversion(
                    x => x.Value,
                    x => new Role(x))
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Claims)
               .HasConversion(x => JsonSerializer.Serialize(x, SerializerOptions),
                   x => JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(x, SerializerOptions));

            builder.Property(x => x.Claims).Metadata.SetValueComparer(
                new ValueComparer<Dictionary<string, IEnumerable<string>>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary(x => x.Key, x => x.Value)));

        }
    }
}