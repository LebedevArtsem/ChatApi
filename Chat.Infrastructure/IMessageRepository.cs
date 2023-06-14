using Chat.Domain;

namespace Chat.Infrastructure;

public interface IMessageRepository
{
    Task<Message> GetByIdAsync(int id, CancellationToken token);

    Task<Message> CreateAsync(Message message, CancellationToken token);

    Task UpdateAsync(int id, Message message, CancellationToken token);

    Task DeleteAsync(int id, CancellationToken token);
}

