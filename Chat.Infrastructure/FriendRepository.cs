using Chat.Domain;
using Chat.Infrastructure.DataBaseConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Chat.Infrastructure;

public class FriendRepository : IFriendRepository
{
    private readonly DataContext _context;

    public FriendRepository(DataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Friend friend, CancellationToken token)
    {
        await _context.Friends
            .AddAsync(friend);

        await _context
            .SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int friendId, CancellationToken token)
    {
        await _context.Friends
            .Where(x =>
                x.UserId == userId &&
                x.UserFriend.Id == friendId)
            .ExecuteDeleteAsync(token);

        await _context
            .SaveChangesAsync(token);
    }

    public Task<List<Friend>> GetAsync(CancellationToken token)
    {
        return
            _context.Friends
            .ToListAsync();
    }

    public async Task<List<User>> GetByUserIdAsync(User user, string key, CancellationToken token)
    {
        await _context
            .Entry(user)
            .Collection(x => x.Friends)
            .LoadAsync(token);

        return user.Friends.Select(x => x.User).ToList(); // todo
    }
    public Task UpdateAsync(int userId, User friend, CancellationToken token)
    {
        return
            _context.Friends
            .Where(x => x.UserId == userId && x.UserFriend.Id == friend.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.UserFriend, friend));
    }
}

