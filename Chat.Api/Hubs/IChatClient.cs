using Chat.Domain;
using System.Threading.Tasks;

namespace Chat.Api.Hubs;
public interface IChatClient
{
    Task ReceiveMessage(ChatMessage message);

    Task SendMessage(string message);
}

