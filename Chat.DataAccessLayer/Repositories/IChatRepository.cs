using Chat.Domain;

namespace Chat.DataAccessLayer.Repositories;

public interface IChatRepository
{
    Task<ICollection<Domain.Chat>> GetChatHistoryAsync(int sender, int reciever, int skip, int take, CancellationToken token);

    Task<Domain.Chat> GetByMessageIdAsync(int messageId, CancellationToken token);

    Task<ICollection<Domain.Chat>> GetAllMessagesToOfflineUserAsync(int userId, CancellationToken token);

    Task CreateAsync(Domain.Chat message, CancellationToken token);

    Task UpdateAsync(int messageId, Message message, CancellationToken token);

    Task DeleteAsync(int messageId, CancellationToken token);
}

