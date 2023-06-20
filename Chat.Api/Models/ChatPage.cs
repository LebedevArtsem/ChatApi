using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class ChatPage
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Wrong value")]
    public int PageSkip { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Wrong value")]
    public int PageTake { get; set; }
}

