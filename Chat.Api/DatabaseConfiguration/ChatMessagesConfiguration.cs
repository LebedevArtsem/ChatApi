using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Api.DatabaseConfiguration
{
    public class ChatMessagesConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .ToTable("chat_messages");

            builder
                .HasKey(x => x.Id)
                .HasName("id");

            
        }
    }
}
