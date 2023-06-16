using AutoMapper;
using Chat.Api.Models;
using Chat.Domain;
using Chat.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/chat")]
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

    [HttpGet("chat-history")]
    public async Task<ActionResult<ICollection<Chat.Domain.Chat>>> GetChatHistory(
        [FromQuery] ChatHistory chatHistory, CancellationToken token)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email); // todo

        var userTask = _users.GetByEmailAsync(userEmail, token);
        var friendTask = _users.GetByEmailAsync(chatHistory.FriendEmail, token);
        await Task.WhenAll(userTask, friendTask);

        var messageList = await
            _chats
            .GetChatHistoryAsync(userTask.Result.Id, friendTask.Result.Id, chatHistory.PageSkip, chatHistory.PageTake, token);

        var messageListResponse = messageList.Select(_mapper.Map<ChatResponse>);

        return Ok(messageListResponse);
    }

    [HttpGet("find-friends")]
    public async Task<ActionResult<ICollection<FriendResponse>>> RequiredFriends([FromQuery] string key, CancellationToken token)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email); // todo

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

    [HttpPost("add-friend")]
    public async Task<ActionResult> AddToFriend([FromBody] FriendRequest friend, CancellationToken token)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

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
}