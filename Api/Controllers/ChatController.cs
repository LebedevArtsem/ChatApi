using Api.Hubs;
using Api.Hubs.Clients;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly DataContext database;

        public ChatController(IHubContext<ChatHub> chatHub, DataContext database)
        {
            _chatHub = chatHub;
            this.database = database;
        }

        [HttpGet("friends")]
        public async Task<List<FriendModel>> Friends()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            //if (user == null) { return Results.NotFound(); }

            var friends = await database.Friends
                .Where(t => t.UserEmail == userEmail)
                .Join(database.Users,
                f => f.UserEmail,
                u => u.Email, (f, u) => new
                {
                    UserEmail = userEmail,
                    FriendEmail = f.Friends.Email,
                    Name = f.Friends.Name
                })
                .Select(t => new FriendModel { FriendEmail = t.FriendEmail, Name = t.Name })
                .ToListAsync();

            return friends;
        }

        [HttpGet("message-history")]
        public async Task<List<ChatMessage>> MessageHistory(string friendEmail)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var messageList = await database.ChatMessages
                .Where(t => 
                (t.SendedUser == userEmail && t.RecievedUser == friendEmail) || 
                (t.RecievedUser == userEmail && t.SendedUser == friendEmail))
                .Take(50)
                .ToListAsync();

            return messageList;
        }
    }
}
