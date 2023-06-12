using Chat.Domain;

namespace Chat.Infrastructure;

public interface IChatMessageRepository
{
    Task<ICollection<ChatMessage>> GetChatHistoryAsync(int sender, int reciever, CancellationToken token);

    Task<ChatMessage> GetByMessageIdAsync(int messageId, CancellationToken token);

    Task CreateAsync(ChatMessage message, CancellationToken token);

    Task UpdateAsync(int messageId, Message message, CancellationToken token);

    Task DeleteAsync(int messageId, CancellationToken token);
}

