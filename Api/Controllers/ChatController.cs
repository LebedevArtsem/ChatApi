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
    public class ChatController:ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly DataContext database;

        public ChatController(IHubContext<ChatHub> chatHub,DataContext database)
        {
            _chatHub = chatHub;
            this.database = database;
        }

        [HttpGet("friends")]
        public async Task<List<User>> Friends()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            //if (user == null) { return Results.NotFound; }

            var friends = await database.Friends
                .Join(database.Users,
                f => f.UserEmail,
                u => u.Email, (f, u) => new Friend
                {
                   Friends = f.Friends,
                   UserEmail = u.Email
                })
                .Where(t => t.UserEmail == userEmail).Select(t=>t.Friends).ToListAsync();

            return friends;
        }

        //[HttpPost("sendmessage")]
        //public async Task SendToUser(MessageModel messageModel)
        //{
        //    await _chatHub.Clients.All.SendAsync("", messageModel.Message);
        //}

    }
}
