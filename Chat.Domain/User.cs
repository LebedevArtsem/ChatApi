﻿namespace Chat.Domain;
public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Friend> Friends { get; set; }
}
