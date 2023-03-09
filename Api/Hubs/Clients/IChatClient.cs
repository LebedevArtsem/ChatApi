using Api.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);

        Task SendMessage(string message);
    }
    public interface A
    {
        public void Send();
    }
    interface B:A
    {
        void Send();
    }
}
