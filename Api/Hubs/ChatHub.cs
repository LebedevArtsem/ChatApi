using Api.Hubs.Clients;
using Api.Infrasrtucture;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly DataContext dataContext;

        public ChatHub(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public string GetConnectionId() => Context.ConnectionId;

        public async Task SendToChatAsync(MessageModel message)
        {

            var user = await dataContext.Users.Where(item => item.Email == message.RecievedUser).SingleOrDefaultAsync();

            if (user == null) { return; }

            var connectionsId = _connections.GetConnection(user.Email);

            if (connectionsId != null)
            {
                foreach (var connection in connectionsId)
                {
                    await Clients.Client(connection).SendAsync("sendtochatasync", message);
                }
            }

            await dataContext.ChatMessages.AddAsync(new ChatMessage()
            {
                Message = message.Message,
                SendedUser = message.SendedUser,
                RecievedUser = message.RecievedUser,
                IsRead = false,
                Time = DateTime.UtcNow
            });

            await dataContext.SaveChangesAsync();
        }

        public override async Task OnConnectedAsync()
        {
            var identity = Context.User.Identity as ClaimsIdentity;

            string email = identity.FindFirst(ClaimTypes.Email).Value;

            _connections.AddConnection(email, Context.ConnectionId);

            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected",_connections.GetAllConnections());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var identity = Context.User.Identity as ClaimsIdentity;

            string email = identity.FindFirst(ClaimTypes.Email).Value;

            _connections.RemoveConnection(email, Context.ConnectionId);

            await Clients.Client(Context.ConnectionId).SendAsync("OnDisconnected", _connections.GetAllConnections());

            await base.OnDisconnectedAsync(exception);
        }

    }
}
