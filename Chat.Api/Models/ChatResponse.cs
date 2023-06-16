namespace Chat.Api.Models;

public class ChatResponse
{
    public int Id { get; set; }

    public string Message { get; set; }

    public UserApi User { get; set; }

    public UserApi Friend { get; set; }
}

