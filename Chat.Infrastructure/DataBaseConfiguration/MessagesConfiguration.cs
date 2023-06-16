using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccessLayer.DatabaseConfiguration;
public class MessagesConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .ToTable("messages");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.Text)
            .HasColumnName("text")
            .IsRequired();

        builder
            .Property(x => x.Time)
            .HasColumnName("time")
            .IsRequired();

        builder
            .Property(x => x.IsRead)
            .HasColumnName("is_read")
            .HasDefaultValue(value: false);

        builder
            .Property(x => x.IsChanged)
            .HasColumnName("is_changed")
            .HasDefaultValue(value: false);

    }
}

