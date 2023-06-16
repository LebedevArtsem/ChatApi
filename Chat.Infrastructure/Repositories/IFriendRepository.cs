using Chat.Domain;

namespace Chat.DataAccessLayer.Repositories;
public interface IFriendRepository
{
    Task<List<Friend>> GetAsync(CancellationToken token);

    Task<ICollection<Friend>> GetByUserIdAsync(User user, string key, CancellationToken token);

    Task<int> CreateAsync(Friend friend, CancellationToken token);

    Task UpdateAsync(int userId, User friend, CancellationToken token);

    Task DeleteAsync(int userId, int friendId, CancellationToken token);
}
