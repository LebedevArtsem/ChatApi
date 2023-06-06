using Chat.Domain;

namespace Chat.Infrastructure;

public interface IChatMessageRepository
{
    Task<ICollection<ChatMessage>> GetChatHistoryAsync(CancellationToken token);

    Task<ChatMessage> GetById(int id);

    Task Create(ChatMessage message);

    Task Update(int id, ChatMessage message);

    Task Delete(int id);
}

