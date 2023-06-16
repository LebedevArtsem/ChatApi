using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccessLayer.DatabaseConfiguration;
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
            .Property(x => x.Email)
            .HasColumnName("email");

        builder
            .HasAlternateKey(x => x.Email);

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .Property(x => x.Hash)
            .HasColumnName("hash")
            .IsRequired();

        builder
            .HasOne(x => x.Token)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x=>x.TokenId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(x => x.TokenId)
            .HasColumnName("token_id");
    }
}

