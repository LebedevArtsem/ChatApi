using Chat.Domain;
using Chat.Infrastructure.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user, CancellationToken token)
    {
        await _context.Users
            .AddAsync(user, token)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync(token));
    }

    public Task<List<User>> GetAsync(CancellationToken token)
    {
        return
            _context.Users
            .ToListAsync(token);
    }

    public Task<User> GetByEmailAsync(string email, CancellationToken token)
    {
        return
            _context.Users
            .SingleAsync(u => u.Email == email, token);
    }

    public Task<User> GetByIdAsync(int id, CancellationToken token)
    {
        return
            _context.Users
            .SingleAsync(u => u.Id == id, token);
    }

    public Task UpdateAsync(int id, User user, CancellationToken token)
    {
        return
            _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x, user), token);
    }
}

