using Chat.Domain;
using Chat.Infrastructure.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;

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
            .AddAsync(friend)
            .AsTask()
            .ContinueWith(_ => _context.SaveChangesAsync());

    }

    public async Task DeleteAsync(int userId, int friendId, CancellationToken token)
    {
        await _context.Friends
            .Where(x =>
                x.UserId == userId &&
                x.UserFriend.Id == friendId)
            .ExecuteDeleteAsync(token)
            .ContinueWith(async _ =>await _context.SaveChangesAsync(token));

    }

    public Task<List<Friend>> GetAsync(CancellationToken token)
    {
        return
            _context.Friends
            .ToListAsync();
    }

    public async Task<ICollection<Friend>> GetByUserIdAsync(User user, string key, CancellationToken token)
    {
        var friends1 = await _context.Entry(user).Collection(x=>x.Friends).Query().ToListAsync();

        var friends = await
            _context.Friends
            .Include(x => x.UserId)
            .Where(x => x.UserId == user.Id && EF.Functions.Like(x.UserFriend.Name, $"{key}%"))
            //.Select(x => new Models.User { x.User, x.UserFriend })
            .ToListAsync(token) as ICollection<Friend>;

        return friends;
    }
    public Task UpdateAsync(int userId, User friend, CancellationToken token)
    {
        return
            _context.Friends
            .Where(x => x.UserId == userId && x.UserFriend.Id == friend.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.UserFriend, friend));
    }
}

