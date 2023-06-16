using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class FriendRequest
{
    [Required]
    public string FriendEmail { get; set; }
}

