using Chat.Domain;

namespace Chat.DataAccessLayer.Repositories;

public interface ITokenRepository
{
    Task<int> CreateAsync(Token token, CancellationToken cancellationToken);

    Task<Token> GetAsync(int id, CancellationToken cancellationToken);

    Task UpdateAsync(int id, Token token, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}

