using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccessLayer.DatabaseConfiguration;

public class ChatsConfiguration : IEntityTypeConfiguration<Domain.Chat>
{
    public void Configure(EntityTypeBuilder<Domain.Chat> builder)
    {
        builder
            .ToTable("chats");

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasIdentityOptions(startValue: 1);

        builder
            .HasOne(x=>x.Message)
            .WithMany(x => x.Chats)
            .HasForeignKey(x => x.MessageId);

        builder
            .Property(x => x.MessageId)
            .HasColumnName("message_id");

        builder
            .HasOne(x => x.Sender)
            .WithMany(x => x.SenderChats)
            .HasForeignKey(x => x.SenderId);

        builder
            .Property(x => x.SenderId)
            .HasColumnName("sender_id");

        builder
            .HasOne(x => x.Reciever)
            .WithMany(x => x.RecieverChats)
            .HasForeignKey(x => x.RecieverId);

        builder
            .Property(x => x.RecieverId)
            .HasColumnName("reciever_id");
    }
}

