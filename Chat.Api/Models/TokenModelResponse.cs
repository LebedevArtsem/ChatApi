using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class TokenModelResponse
{
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }
}

