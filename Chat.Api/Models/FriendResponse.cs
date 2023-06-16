namespace Chat.Api.Models;

public class FriendResponse
{
    public int Id { get; set; }

    public UserApi Friend { get; set; }

    public UserApi User { get; set; }
}

