using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccessLayer.DatabaseConfiguration;

public class TokensConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder
            .ToTable("tokens");

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasIdentityOptions(startValue: 1);

        builder
            .Property(x => x.RefreshToken)
            .HasColumnName("refresh_token");

        builder
            .Property(x => x.RefreshTokenExpiryTime)
            .HasColumnName("refresh_token_expiry_time");
    }
}

