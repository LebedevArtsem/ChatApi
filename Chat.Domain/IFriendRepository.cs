namespace Chat.Domain;
public interface IFriendRepository
{
    Task<ICollection<Friend>> Get();

    Task<Friend> GetById(int id);

    Task Create(Friend friend);

    Task Update(int id, Friend friend);

    Task Delete(int id);
}
