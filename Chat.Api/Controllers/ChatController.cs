using AutoMapper;
using Chat.Api.Models;
using Chat.Domain;
using Chat.Infrastructure;
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

        var user = _users.GetByEmailAsync(userEmail, token);
        var friend = _users.GetByEmailAsync(chatHistory.FriendEmail, token);
        await Task.WhenAll(user, friend);

        if (user == null || friend == null)
        {
            return Conflict();
        }

        var messageList = await
            _chats
            .GetChatHistoryAsync(user.Result.Id, friend.Result.Id, chatHistory.PageSkip, chatHistory.PageTake, token);

        return Ok(messageList);
    }

    [HttpGet("find-friends")]
    public async Task<ActionResult<ICollection<FriendModelResponse>>> RequiredFriends(string key, CancellationToken token)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email); // todo

        var user = await
            _users
            .GetByEmailAsync(userEmail, token);

        if (user == null)
        {
            return Conflict();
        }

        var friendList = await
            _friends
            .GetByUserIdAsync(user, key, token);

        var friendListResponse =
            friendList
            .AsParallel()// todo
            .Select(_mapper.Map<FriendModelResponse>)
            .ToList();

        return Ok(friendListResponse);
    }

    [HttpPost("add-friend")]
    public async Task<ActionResult> AddToFriend([FromBody] string friendEmail, CancellationToken token)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var user = _users.GetByEmailAsync(userEmail, token);
        var friend = _users.GetByEmailAsync(friendEmail, token);
        await Task.WhenAll(user, friend);

        if (user.Result == null || friend.Result == null)
        {
            return Conflict();
        }

        await _friends
            .CreateAsync(new Friend()
            {
                User = user.Result,
                UserFriend = friend.Result
            },
            token
            );

        return Ok();
    }
}