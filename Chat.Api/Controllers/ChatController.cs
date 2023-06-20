using AutoMapper;
using Chat.Api.Models;
using Chat.Domain;
using Chat.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/chat/users/{userEmail}/friends")]
public class ChatController : ControllerBase
{
    private readonly IFriendRepository _friends;
    private readonly IChatRepository _chats;
    private readonly IUserRepository _users;
    private readonly IMapper _mapper;

    public ChatController(IFriendRepository friends, IChatRepository chats, IUserRepository users, IMapper mapper)
    {
        _friends = friends;
        _chats = chats;
        _users = users;
        _mapper = mapper;
    }

    [HttpGet("{friendEmail}/history")]
    public async Task<ActionResult<ICollection<Chat.Domain.Chat>>> GetChatHistory(
        [FromRoute] string userEmail,
        [FromRoute] string friendEmail,
        [FromQuery] ChatPage chatHistory,
        CancellationToken token)
    {
        var userTask = _users.GetByEmailAsync(userEmail, token);
        var friendTask = _users.GetByEmailAsync(friendEmail, token);
        await Task.WhenAll(userTask, friendTask);
        var list = new List<int>();

        var messageList = await
            _chats
            .GetChatHistoryAsync(userTask.Result.Id, friendTask.Result.Id, chatHistory.PageSkip, chatHistory.PageTake, token);

        var messageListResponse = messageList.Select(_mapper.Map<ChatResponse>);

        return Ok(messageListResponse);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<FriendResponse>>> RequiredFriends(
        [FromRoute] string userEmail,
        [FromQuery] string key,
        CancellationToken token)
    {
        var user = await
            _users
            .GetByEmailAsync(userEmail, token);

        var friendList = await
            _friends
            .GetByUserIdAsync(user, key, token);

        var friendListResponse =
            friendList
            .Select(_mapper.Map<FriendResponse>)
            .ToList();

        return Ok(friendListResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddToFriend(
        [FromRoute] string userEmail,
        [FromBody] FriendRequest friend,
        CancellationToken token)
    {
        var userTask = _users.GetByEmailAsync(userEmail, token);
        var friendTask = _users.GetByEmailAsync(friend.FriendEmail, token);
        await Task.WhenAll(userTask, friendTask);

        await _friends
            .CreateAsync(new Friend()
            {
                User = userTask.Result,
                UserFriend = friendTask.Result
            },
            token);

        return Ok();
    }

    [HttpDelete("{friendEmail}")]
    public async Task<ActionResult> RemoveFromFriends(
        [FromRoute] string userEmail,
        [FromRoute] string friendEmail,
        CancellationToken token)
    {
        var userTask = _users.GetByEmailAsync(userEmail, token);
        var friendTask = _users.GetByEmailAsync(friendEmail, token);
        await Task.WhenAll(userTask, friendTask);

        await _friends
            .DeleteAsync(userTask.Result.Id, friendTask.Result.Id, token);

        return Ok();
    }
}