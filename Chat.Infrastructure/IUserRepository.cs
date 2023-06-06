using Chat.Domain;

namespace Chat.Infrastructure;
public interface IUserRepository
{
    Task<ICollection<User>> Get();

    Task<User> GetById(int id);

    Task Create(User user);

    Task Update(int id, User user);
}

