using Chat.Domain;
using Chat.Infrastructure.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _context;

    public MessageRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Message> CreateAsync(Message message, CancellationToken token)
    {
        var insertedMessage = await _context.Messages.AddAsync(message, token);

        await _context.SaveChangesAsync();

        return insertedMessage.Entity;
    }

    public Task DeleteAsync(int id, CancellationToken token)
    {
        return
            _context.Messages
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public Task<Message> GetByIdAsync(int id, CancellationToken token)
    {
        return
            _context.Messages
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task UpdateAsync(int id, Message message, CancellationToken token)
    {
        return
            _context.Messages
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x, message));
    }
}

