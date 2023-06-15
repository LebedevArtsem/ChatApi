using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class ChatHistory
{
    [Required]
    public string FriendEmail { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Wrong value")]
    public int PageSkip { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Wrong value")]
    public int PageTake { get; set; }
}

