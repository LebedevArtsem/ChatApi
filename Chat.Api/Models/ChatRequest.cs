using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class ChatRequest
{
    [Required]
    public string Message { get; set; }

    [Required]
    public string RecieverEmail { get; set; }

    [Required]
    public string SenderEmail { get; set; }
}

