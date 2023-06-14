using Chat.Api.Common;
using Chat.Api.Models;
using Chat.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : Controller
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokenService;

    public TokenController(IUserRepository users, ITokenService tokenService)
    {
        _users = users;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IResult> Refresh(TokenModelResponse tokenModel, CancellationToken token)
    {
        if (tokenModel is null)
        {
            return Results.BadRequest();
        }

        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        var email = principal.FindFirst(ClaimTypes.Email).Value;

        var user = await _users.GetByEmailAsync(email, token);

        if (user == null ||
            user.RefreshToken != tokenModel.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("Invalid client request");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
        await _users.UpdateAsync(user.Id, user, token);

        var response = new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return Results.Ok(response);
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IResult> Revoke(CancellationToken token)
    {
        var email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;

        var user = await _users.GetByEmailAsync(email, token);
        if (user == null)
            return Results.BadRequest();

        user.RefreshToken = null;
        await _users.UpdateAsync(user.Id, user, token);

        return Results.NoContent();
    }
}

