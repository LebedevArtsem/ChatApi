using Chat.Domain;

namespace Chat.Infrastructure;
public class UserRepository : IUserRepository
{
    //private readonly

    public Task Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, User user)
    {
        throw new NotImplementedException();
    }
}

