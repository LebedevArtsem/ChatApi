namespace Chat.Domain;
public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Hash { get; set; }

    public int TokenId { get; set; }

    public Token Token { get; set; }

    public ICollection<Friend> Friends { get; set; }

    public ICollection<Chat> SenderChats { get; set; }

    public ICollection<Chat> RecieverChats { get; set; }
}
