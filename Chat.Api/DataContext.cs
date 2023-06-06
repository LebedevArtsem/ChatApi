using Chat.Api.DatabaseConfiguration;
using Chat.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api
{
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
}
