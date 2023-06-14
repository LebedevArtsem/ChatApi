using Chat.Api.Models;
using Chat.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Api.Hubs;
public interface IChatClient
{
    Task OnReceiveMessage(ICollection<Domain.Chat> messages);

    Task OnSendMessage(ChatRequest message);

    Task OnUserNotFound();

    Task OnMessageNotFount();

    Task OnChangeMessage(int id);

    Task OnDeleteMessage(int id);

    Task OnReadMessage(int id);
}

