using Chat.Domain;
using Chat.Infrastructure.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Chat.Infrastructure;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly DataContext _context;

    public ChatMessageRepository(DataContext context, CancellationToken token)
    {
        _context = context;
    }

    public Task CreateAsync(ChatMessage message, CancellationToken token)
    {
        return
            _context.ChatMessages
            .AddAsync(message, token)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync(token));
    }

    public Task DeleteAsync(int messageId, CancellationToken token)
    {
        return
            _context.ChatMessages
            .Where(x => x.MessageId == messageId)
            .ExecuteDeleteAsync(token);
    }

    public Task<ChatMessage> GetByMessageIdAsync(int messageId, CancellationToken token)
    {
        return
            _context.ChatMessages
            .SingleOrDefaultAsync(x => x.MessageId == messageId, token);
    }

    public async Task<ICollection<ChatMessage>> GetChatHistoryAsync(int sender, int reciever, CancellationToken token)
    {
        var chat = await _context.ChatMessages
            .Where(x => x.SenderId == sender && x.RecieverId == reciever)
            .Include(x => x.Message)
            .Include(x => x.Sender)
            .Include(x => x.Reciever)
            .ToListAsync(token);

        return chat;
    }

    public Task UpdateAsync(int messageId, Message message, CancellationToken token)
    {
        return
            _context.ChatMessages
            .Where(x => x.MessageId == messageId)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Message, message), token);
    }
}

