using Chat.Domain;

namespace Chat.DataAccessLayer.Repositories;
public interface IUserRepository
{
    Task<List<User>> GetAsync(CancellationToken token);

    Task<User> GetByIdAsync(int id, CancellationToken token);

    Task<User> GetByEmailAsync(string email, CancellationToken token);

    Task<int> CreateAsync(User user, CancellationToken token);

    Task UpdateAsync(int id, User user, CancellationToken token);
}

