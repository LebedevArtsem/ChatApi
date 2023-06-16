using Chat.Domain;
using Chat.DataAccessLayer.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccessLayer.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;

    public ChatRepository(DataContext context)
    {
        _context = context;
    }

    public Task CreateAsync(Domain.Chat message, CancellationToken token)
    {
        return
            _context.Chats
            .AddAsync(message, token)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync(token));
    }

    public Task DeleteAsync(int messageId, CancellationToken token)
    {
        return
            _context.Chats
            .Where(x => x.MessageId == messageId)
            .ExecuteDeleteAsync(token);
    }

    public async Task<ICollection<Domain.Chat>> GetAllMessagesToOfflineUserAsync(int userId, CancellationToken token)
    {
        var chats = await
            _context.Chats
            .Where(x => x.RecieverId == userId && x.Message.IsRead == false)
            .Include(x => x.Message)
            .Include(x => x.Sender)
            .Include(x => x.Reciever)
            .ToListAsync(token);

        return chats;
    }

    public Task<Domain.Chat> GetByMessageIdAsync(int messageId, CancellationToken token)
    {
        return
            _context.Chats
            .SingleOrDefaultAsync(x => x.MessageId == messageId, token);
    }

    public async Task<ICollection<Domain.Chat>> GetChatHistoryAsync(int sender, int reciever, int skip, int take, CancellationToken token)
    {
        var chat = await _context.Chats
            .Where(x => x.SenderId == sender && x.RecieverId == reciever)
            .Include(x => x.Message)
            .Include(x => x.Sender)
            .Include(x => x.Reciever)
            .Skip(skip)
            .Take(take)
            .ToListAsync(token);

        return chat;
    }

    public Task UpdateAsync(int messageId, Message message, CancellationToken token)
    {
        return
            _context.Chats
            .Where(x => x.MessageId == messageId)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Message, message), token);
    }
}

