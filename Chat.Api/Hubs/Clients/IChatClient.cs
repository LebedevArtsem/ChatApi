using Chat.Domain;
using System.Threading.Tasks;

namespace Api.Hubs.Clients;
public interface IChatClient
{
    Task ReceiveMessage(ChatMessage message);

    Task SendMessage(string message);
}

