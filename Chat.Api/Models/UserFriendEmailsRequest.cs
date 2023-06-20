using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class UserFriendEmailsRequest
{
    [Required]
    public string UserEmail { get; set; }

    [Required]
    public string FriendEmail { get; set; }
}

