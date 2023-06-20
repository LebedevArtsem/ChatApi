using Chat.Domain;
using Chat.DataAccessLayer.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccessLayer.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public Task<int> CreateAsync(User user, CancellationToken token)
    {
        return _context.Users
            .AddAsync(user, token)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync(token))
            .Unwrap();
    }

    public Task<List<User>> GetAsync(CancellationToken token)
    {
        return
            _context.Users
            .AsNoTracking()
            .ToListAsync(token);
    }

    public Task<User> GetByEmailAsync(string email, CancellationToken token)
    {
        return
            _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email, token);
    }

    public Task<User> GetByIdAsync(int id, CancellationToken token)
    {
        return
            _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, token);
    }

    public Task UpdateAsync(int id, User user, CancellationToken token)
    {
        return
            _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(
                x => x
                .SetProperty(x => x.Email, user.Email)
                .SetProperty(x=>x.Name,user.Name)
                .SetProperty(x=>x.Hash,user.Hash)
                .SetProperty(x=>x.TokenId,user.TokenId),
                token);
    }
}