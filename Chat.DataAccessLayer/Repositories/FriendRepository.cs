using Chat.Domain;
using Chat.DataAccessLayer.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccessLayer.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly DataContext _context;

    public FriendRepository(DataContext context)
    {
        _context = context;
    }

    public Task<int> CreateAsync(Friend friend, CancellationToken token)
    {
        return _context.Friends
            .AddAsync(friend)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync()).Unwrap();

    }

    public Task DeleteAsync(int userId, int friendId, CancellationToken token)
    {
        return _context.Friends
            .Where(x =>
                x.UserId == userId &&
                x.UserFriend.Id == friendId)
            .ExecuteDeleteAsync(token)
            .ContinueWith(async _ => await _context.SaveChangesAsync(token));

    }

    public Task<List<Friend>> GetAsync(CancellationToken token)
    {
        return
            _context.Friends
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<Friend>> GetByUserIdAsync(User user, string key, CancellationToken token)
    {
        var friends = await
            _context.Friends
            .AsNoTracking()
            .Include(x => x.UserId)
            .Where(x => x.UserId == user.Id && EF.Functions.Like(x.UserFriend.Name, $"{key}%"))
            .ToListAsync(token) as ICollection<Friend>;

        return friends;
    }

    public Task UpdateAsync(int userId, User friend, CancellationToken token)
    {
        return
            _context.Friends
            .Where(x => x.UserId == userId && x.UserFriend.Id == friend.Id)
            .ExecuteUpdateAsync(
                x => x
                .SetProperty(x => x.UserFriend, friend),
                token);
    }
}

