
namespace Chat.Domain;
public class Message
{
    public int Id { get; set; }

    public string Text { get; set; }

    public DateTime Time { get; set; }

    public bool IsRead { get; set; }

    public bool IsChanged { get; set; }

    public ICollection<Chat> Chats { get; set; }
}

