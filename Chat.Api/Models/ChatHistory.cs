using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class ChatHistory
{
    [Required]
    public string FriendEmail { get; set; }

    [Required]
    public int PageSkip { get; set; }

    [Required]
    public int PageTake { get; set; }
}

