using Api.Hubs.Clients;
using Api.Infrasrtucture;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub/*<IChatClient>*/
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly DataContext dataContext;

        public ChatHub(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        //public async Task Send(ChatMessage message)
        //{
        //    await Clients.All.ReceiveMessage(message);
        //}

        public string GetConnectionId() => Context.ConnectionId;

        public async Task SendMes(string message)
        {
            await Clients.All.SendAsync("sendmes", message);
        }

        public async Task SendToChat(MessageModel message)
        {

            var user = await dataContext.Users.Where(item => item.Email == message.Message).SingleOrDefaultAsync();

            if (user == null) { return; }

            var connectionsId = _connections.GetConnection(user.Email);

            //add if user is not online

            foreach (var connection in connectionsId)
            {
                await Clients.Client(connection).SendAsync("sendtochat", message.Message);
            }
        }

        public override Task OnConnectedAsync()
        {
            var identity = Context.User.Identity as ClaimsIdentity;

            string email = identity.FindFirst(ClaimTypes.Email).Value;

            _connections.AddConnection(email, Context.ConnectionId);

            return base.OnConnectedAsync();
        }
    }
}
