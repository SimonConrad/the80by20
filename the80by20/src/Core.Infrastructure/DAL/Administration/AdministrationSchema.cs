using Core.App.Administration.MasterData;
using Core.App.Administration.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL.Administration;

public class AdministrationSchema
{
    public static void MapUsing(ModelBuilder builder)
    {
        builder.Entity<Category>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
        });

        builder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id)
                .HasConversion(
                    x => x.Value, 
                    x => new UserId(x));

            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email)
                .HasConversion(
                    x => x.Value, 
                    x => new Email(x))
                .IsRequired()
                .HasMaxLength(100);
            
            e.HasIndex(x => x.Username).IsUnique();
            e.Property(x => x.Username)
                .HasConversion(
                    x => x.Value, 
                    x => new Username(x))
                .IsRequired()
                .HasMaxLength(30);
            
            e.Property(x => x.Password)
                .HasConversion(
                    x => x.Value,
                    x => new Password(x))
                .IsRequired()
                .HasMaxLength(200);

            e.Property(x => x.FullName)
                .HasConversion(
                    x => x.Value, 
                    x => new FullName(x))
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.Role)
                .HasConversion(
                    x => x.Value, 
                    x => new Role(x))
                .IsRequired()
                .HasMaxLength(30);

            e.Property(x => x.CreatedAt)
                .IsRequired();
        });
    }
}