using Chat.Api.Models;
using Chat.Domain;
using Chat.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Api.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly IChatRepository _chats;
    private readonly IUserRepository _users;
    private readonly IMessageRepository _messages;

    public ChatHub(IUserRepository users, IChatRepository chat, IMessageRepository messages)
    {
        _users = users;
        _chats = chat;
        _messages = messages;
    }

    public string GetConnectionId() => Context.ConnectionId;

    public async Task SendToChatAsync(ChatRequest chat)
    {
        var token = CancellationToken.None;

        var reciever = _users.GetByEmailAsync(chat.RecieverEmail, token);
        var sender = _users.GetByEmailAsync(chat.SenderEmail, token);

        await Task.WhenAll(reciever, sender);

        if (reciever == null || sender == null)
        {
            await Clients.Caller.OnUserNotFound();
            return;
        }

        var message = await _messages
            .CreateAsync(
            new Message()
            {
                Text = chat.Message,
                Time = DateTime.UtcNow,
            },
            token
            );

        await Clients.Group(reciever.Result.Email).OnSendMessage(chat);

        await _chats.CreateAsync(
            new Domain.Chat()
            {
                Message = message,
                Sender = sender.Result,
                Reciever = reciever.Result,
            },
            token
            );
    }

    public async Task RecieveFromChatAsync()
    {
        var token = CancellationToken.None;

        var email = GetUserEmail();

        var user = await _users.GetByEmailAsync(email, token);

        if (user == null)
        {
            await Clients.Caller.OnUserNotFound();
            return;
        }

        var messages = await _chats.GetAllMessagesToOfflineUserAsync(user.Id, token);

        await Clients.Caller.OnReceiveMessage(messages);
    }

    public override async Task OnConnectedAsync()
    {
        var email = GetUserEmail();

        await Groups.AddToGroupAsync(Context.ConnectionId, email);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var email = GetUserEmail();

        await Groups.RemoveFromGroupAsync(email, Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }

    private string GetUserEmail()
    {
        var identity = Context.User.Identity as ClaimsIdentity;

        return identity.FindFirst(ClaimTypes.Email).Value;
    }
}

