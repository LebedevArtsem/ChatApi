using Chat.Domain;

namespace Chat.Infrastructure;
public interface IFriendRepository
{
    Task<List<Friend>> GetAsync(CancellationToken token);

    Task<List<User>> GetByUserIdAsync(User user, string key, CancellationToken token);

    Task CreateAsync(Friend friend, CancellationToken token);

    Task UpdateAsync(int userId, User friend, CancellationToken token);

    Task DeleteAsync(int userId, int friendId, CancellationToken token);
}
