namespace Chat.Infrastructure.Models;

public class UserDto    
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public List<UserDto> Friends { get; set; }
}

