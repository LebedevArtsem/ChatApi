
namespace Chat.Domain;

public class Chat
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public Message Message { get; set; }

    public int SenderId { get; set; }

    public User Sender { get; set; }

    public int RecieverId { get; set; }

    public User Reciever { get; set; }
}

