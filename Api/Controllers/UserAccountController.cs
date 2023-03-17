using Api.Infrasrtucture;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserAccountController : Controller
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public UserAccountController(DataContext context, AppSettings appSettings)
        {
            _context = context;
            _tokenService = new TokenService(appSettings);
        }

        [HttpPost("signin")]
        public async Task<IResult> SignIn([FromBody] SignInUser signInUser)
        {
            if (!ModelState.IsValid) return Results.BadRequest("Error");

            var user = await _context.Users
                .Where(item => item.Email == signInUser.Email)
                .FirstOrDefaultAsync();

            if (user == null) return Results.BadRequest("Please pass the valid Email");

            var passwordValid = Crypto.VerifyHashedPassword(user.Password, signInUser.Password);
            if (!passwordValid)
                return Results.BadRequest("Please pass the valid Password");

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

            _context.SaveChanges();

            

            return Results.Ok(response);
        }

        [HttpPost("signup")]
        public async Task<IResult> SignUp([FromBody] SignUpUser signUpUser)
        {
            if (!ModelState.IsValid) return Results.BadRequest("Error");

            var user = _context.Users.Where(item => item.Email == signUpUser.Email).FirstOrDefault();

            if (user == null)
            {
                user = new User()
                {
                    Email = signUpUser.Email,
                    Name = signUpUser.Name,
                    Password = Crypto.HashPassword(signUpUser.Password)
                };

                await _context.AddAsync(user);
                _context.SaveChanges();

                return Results.Ok("Fine");
            }
            else
            {
                return Results.BadRequest("Error");
            }
            static void Send()
            {

            }
        }
    }
}
