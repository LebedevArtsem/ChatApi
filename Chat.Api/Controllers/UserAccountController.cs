using Chat.Api;
using Chat.Api.Common;
using Chat.Api.Models;
using Chat.Domain;
using Chat.Infrastructure;
using Microsoft.AspNetCore.Http;
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
public class UserAccountController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _users;

    public UserAccountController(IUserRepository users, AppSettings appSettings)
    {
        _users = users;
        _tokenService = new TokenService(appSettings);
    }

    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInUser signInUser, CancellationToken token)
    {
        var user = await _users.GetByEmailAsync(signInUser.Email, token);

        if (user == null) 
            return Conflict("User is not found");

        var hasher = new PasswordHasher<User>();

        var passwordValid = hasher.VerifyHashedPassword(user, user.Password, signInUser.Password);
        if (passwordValid == PasswordVerificationResult.Failed)
            return BadRequest("Please pass the valid Password");// todo: Wrong answer

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var response = new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

        await _users.UpdateAsync(user.Id, user, token);

        return Ok(response);
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp([FromBody] SignUpUser signUpUser, CancellationToken token)
    {
        var user = await _users.GetByEmailAsync(signUpUser.Email, token);

        if (user == null)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user = new User()
            {
                Email = signUpUser.Email,
                Name = signUpUser.Name,
                Password = hasher.HashPassword(user, signUpUser.Password)
            };

            await _users.CreateAsync(user, token);

            return Ok();
        }
        else
        {
            return Conflict();
        }
    }
}

