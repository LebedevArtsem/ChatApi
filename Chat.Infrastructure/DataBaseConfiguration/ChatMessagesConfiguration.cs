using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.DatabaseConfiguration;

public class ChatMessagesConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder
            .ToTable("chat_messages");

        builder
            .HasKey(x => x.Id)
            .HasName("id");

        builder
            .HasOne<Message>()
            .WithOne()
            .HasForeignKey<Message>(x => x.Id);

        builder
            .Property(x => x.MessageId)
            .HasColumnName("message_id");

        builder
            .HasOne(x => x.Sender)
            .WithOne()
            .HasForeignKey<User>(x => x.Id);

        builder
            .Property(x => x.SenderId)
            .HasColumnName("sender_id");

        builder
            .HasOne(x => x.Reciever)
            .WithOne()
            .HasForeignKey<User>(x => x.Id);

        builder
            .Property(x => x.RecieverId)
            .HasColumnName("reciever_id");

    }
}

