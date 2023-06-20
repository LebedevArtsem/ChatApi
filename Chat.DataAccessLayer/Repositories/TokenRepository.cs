using Chat.DataAccessLayer.DatabaseConfiguration;
using Chat.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccessLayer.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly DataContext _context;

    public TokenRepository(DataContext context)
    {
        _context = context;
    }
    public Task<int> CreateAsync(Token token, CancellationToken cancellationToken)
    {
        return
            _context.Tokens
            .AddAsync(token, cancellationToken)
            .AsTask()
            .ContinueWith(async _ => await _context.SaveChangesAsync(cancellationToken))
            .Unwrap();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _context.Tokens.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);

        await _context.SaveChangesAsync();
    }

    public Task<Token> GetAsync(int id, CancellationToken cancellationToken)
    {
        return
            _context.Tokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(int id, Token token, CancellationToken cancellationToken)
    {
        return
            _context.Tokens
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(
                x => x
                .SetProperty(x => x.RefreshToken, token.RefreshToken)
                .SetProperty(x => x.RefreshTokenExpiryTime, token.RefreshTokenExpiryTime),
                cancellationToken);
    }
}

