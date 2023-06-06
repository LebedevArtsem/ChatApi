using Chat.Domain;

namespace Chat.Infrastructure;
public interface IFriendRepository
{
    Task<ICollection<Friend>> GetAsync(CancellationToken token);

    Task<Friend> GetByIdAsync(int id, CancellationToken token);

    Task CreateAsync(Friend friend, CancellationToken token);

    Task UpdateAsync(int id, Friend friend, CancellationToken token);

    Task DeleteAsync(int id, CancellationToken token);
}
