using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    //private readonly DataContext database;

    //public ChatController(DataContext database)
    //{
    //    this.database = database;
    //}

    //[HttpGet("friends")]
    //public async Task<List<FriendModel>> Friends()
    //{
    //    var userEmail = User.FindFirstValue(ClaimTypes.Email);

    //    //if (user == null) { return Results.NotFound(); }

    //    var friends = await database.Friends
    //        .Where(t => t.UserEmail == userEmail)
    //        .Join(database.Users,
    //        f => f.UserEmail,
    //        u => u.Email, (f, u) => new
    //        {
    //            UserEmail = userEmail,
    //            FriendEmail = f.Friends.Email,
    //            Name = f.Friends.Name
    //        })
    //        .Select(t => new FriendModel { FriendEmail = t.FriendEmail, Name = t.Name })
    //        .ToListAsync();

    //    return friends;
    //}

    //[HttpGet("message-history")]
    //public async Task<List<ChatMessage>> MessageHistory([FromQuery] string friendEmail)
    //{
    //    var userEmail = User.FindFirstValue(ClaimTypes.Email);

    //    var messageList =
    //        await database.ChatMessages
    //        .Where(
    //            t =>
    //                (t.SendUser.Email == userEmail && t.RecievedUser.Email == friendEmail) ||
    //                (t.RecievedUser.Email == userEmail && t.SendUser.Email == friendEmail)
    //            )
    //        .Take(50)
    //        .ToListAsync();

    //    return messageList;
    //}

    //[HttpGet("find-friends")]
    //public async Task<List<FriendModel>> RequiredFriends(string key)
    //{
    //    var userEmail = User.FindFirstValue(ClaimTypes.Email);

    //    var friendList = await database.Friends
    //        .Where(x => x.UserEmail == userEmail)
    //        .Join(database.Users,
    //        f => f.FriendEmail,
    //        u => u.Email,
    //        (f, u) => new FriendModel
    //        {
    //            FriendEmail = f.FriendEmail,
    //            Name = u.Name
    //        })
    //        .Where(i => EF.Functions.Like(i.Name, $"{key}%"))
    //        .ToListAsync();


    //    return friendList;
    //}
}