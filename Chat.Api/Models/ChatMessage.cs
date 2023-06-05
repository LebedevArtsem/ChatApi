using Chat.Domain;
using System;

namespace Api.Models;

public class ChatMessage
{
    public int Id { get; set; }

    public User SendUser { get; set; }

    public User RecievedUser { get; set; }

    public string Message { get; set; }

    public DateTime Time { get; set; }

    public bool IsRead { get; set; }

    public bool IsChanged { get; set; }
}

