using Chat.Domain;

namespace Chat.Infrastructure;
public interface IUserRepository
{
    Task<List<User>> GetAsync(CancellationToken token);

    Task<User> GetByIdAsync(int id, CancellationToken token);

    Task CreateAsync(User user, CancellationToken token);

    Task UpdateAsync(int id, User user, CancellationToken token);
}

