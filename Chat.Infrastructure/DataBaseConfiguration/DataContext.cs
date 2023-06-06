using Chat.Domain;
using Chat.Infrastructure.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.DataBaseConfiguration;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<Friend> Friends { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(x => x.MigrationsAssembly("Chat.Infrastructure"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new FriendsConfiguration());
        modelBuilder.ApplyConfiguration(new MessagesConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessagesConfiguration());
    }

}

