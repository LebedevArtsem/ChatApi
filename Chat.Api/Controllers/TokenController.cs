using Chat.Api.Services;
using Chat.Api.Models;
using Chat.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokenService;
    private readonly ITokenRepository _tokens;

    public TokenController(IUserRepository users, ITokenService tokenService, ITokenRepository tokens)
    {
        _users = users;
        _tokenService = tokenService;
        _tokens = tokens;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh([FromBody] TokenModelResponse tokenModel, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        var email = principal.FindFirst(ClaimTypes.Email).Value;

        var user = await _users.GetByEmailAsync(email, cancellationToken);

        var userToken = await _tokens.GetAsync(user.TokenId, cancellationToken);

        if (userToken.RefreshToken != tokenModel.RefreshToken ||
            userToken.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid client request");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        userToken.RefreshToken = newRefreshToken;
        userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
        await _tokens.UpdateAsync(userToken.Id, userToken, cancellationToken);

        var response = new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return CreatedAtAction("Refresh token", response);
    }

    [HttpPost("revoke"), Authorize]
    public async Task<ActionResult> Revoke(CancellationToken cancellationToken)
    {
        var email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;

        var user = await _users.GetByEmailAsync(email, cancellationToken);
        if (user == null)
            return NotFound();

        await _tokens.DeleteAsync(user.TokenId, cancellationToken);

        return NoContent();
    }
}

