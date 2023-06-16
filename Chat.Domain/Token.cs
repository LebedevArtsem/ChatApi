
namespace Chat.Domain;

public class Token
{
    public int Id { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public User User { get; set; }
}

