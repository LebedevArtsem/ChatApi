using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Api.DatabaseConfiguration
{
    public class FriendsConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder
                .ToTable("friends");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .HasOne(x => x.UserFriend)
                .WithMany(x => x.Friends)
                .HasForeignKey("friend_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Friend>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
