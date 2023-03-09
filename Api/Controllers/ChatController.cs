using Api.Hubs;
using Api.Hubs.Clients;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController:ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }

        //[HttpPost("messages")]
        //public async Task Post(ChatMessage message)
        //{
        //    await _chatHub.Clients.All.ReceiveMessage(message);
        //}

        //[HttpPost("sendmessage")]
        //public async Task SendToUser(MessageModel messageModel)
        //{
        //    await _chatHub.Clients.All.SendAsync("", messageModel.Message);
        //}

    }
}
