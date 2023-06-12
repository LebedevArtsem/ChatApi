using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class MessageModel
{
    [Required]
    public string Message { get; set; }

    [Required]
    public string RecievedUser { get; set; }

    [Required]
    public string SendedUser { get; set; }
}

