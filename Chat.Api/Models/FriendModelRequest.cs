﻿using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models;

public class FriendModelRequest
{
    [Required]
    public string FriendEmail { get; set; }

    public string Name { get; set; }
}

