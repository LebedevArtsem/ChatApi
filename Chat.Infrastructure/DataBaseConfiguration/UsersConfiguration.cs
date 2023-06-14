using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Chat.Infrastructure.DatabaseConfiguration;
public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(x => x.Email);

        builder
            .ToTable("users");

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasIdentityOptions(startValue: 1);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Email)
            .HasColumnName("email");


        builder
            .HasAlternateKey(x => x.Email);

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
        .IsRequired();

        builder
            .Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired();

        builder
            .Property(x => x.RefreshToken)
            .HasColumnName("refresh_token");

        builder
            .Property(x => x.RefreshTokenExpiryTime)
            .HasColumnName("refresh_token_expiry_time");

    }
}

