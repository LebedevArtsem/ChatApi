using Chat.Api;
using Chat.Api.Services;
using Chat.Api.Models;
using Chat.Domain;
using Chat.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class UserAccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _users;
    private readonly ITokenRepository _tokens;

    public UserAccountController(IUserRepository users, ITokenRepository tokens, AppSettings appSettings)
    {
        _users = users;
        _tokenService = new TokenService(appSettings);
        _tokens = tokens;
    }

    [HttpPost("sign-in")]

    public async Task<IActionResult> SignIn([FromBody] SignInUser signInUser, CancellationToken cancellationToken)
    {
        var user = await _users.GetByEmailAsync(signInUser.Email, cancellationToken);

        var hasher = new PasswordHasher<Chat.Domain.User>();

        var passwordValid = hasher.VerifyHashedPassword(user, user.Hash, signInUser.Password);
        if (passwordValid == PasswordVerificationResult.Failed)
            return BadRequest("Please pass the valid Password");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email)
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var response = new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        user.Token ??= new Token();

        user.Token.RefreshToken = refreshToken;
        user.Token.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

        await _tokens.UpdateAsync(user.TokenId, user.Token, cancellationToken);

        return Ok(response);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUser signUpUser, CancellationToken cancellationToken)
    {
        var user = await _users.GetByEmailAsync(signUpUser.Email, cancellationToken);

        if (user != null)
        {
            return Conflict();
        }

        var hasher = new PasswordHasher<Chat.Domain.User>();
        user = new Chat.Domain.User()
        {
            Email = signUpUser.Email,
            Name = signUpUser.Name,
            Hash = hasher.HashPassword(user, signUpUser.Password),
            Token = new Token()
        };

        await _users.CreateAsync(user, cancellationToken);

        return Ok();
    }
}

