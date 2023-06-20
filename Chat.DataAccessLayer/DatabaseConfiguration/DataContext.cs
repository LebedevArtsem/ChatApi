using Chat.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccessLayer.DatabaseConfiguration;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Domain.Chat> Chats { get; set; }

    public DbSet<Friend> Friends { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Token> Tokens { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(x => x.MigrationsAssembly("Chat.DataAccessLayer"));
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new FriendsConfiguration());
        modelBuilder.ApplyConfiguration(new MessagesConfiguration());
        modelBuilder.ApplyConfiguration(new ChatsConfiguration());
        modelBuilder.ApplyConfiguration(new TokensConfiguration());
    }

}

