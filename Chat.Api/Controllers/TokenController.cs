using Api.Infrasrtucture;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public TokenController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IResult> Refresh(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return Results.BadRequest();
            }
            
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
            var email = principal.FindFirst(ClaimTypes.Email).Value;

            var user = await _context.Users.Where(item => item.Email == email).SingleOrDefaultAsync();

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
            await _context.SaveChangesAsync();

            var response = new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return Results.Ok(response);
        }

        [HttpPost,Authorize]
        [Route("revoke")]
        public async Task<IResult> Revoke()
        {
            var email = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;

            var user = await _context.Users.Where(item => item.Email == email).SingleOrDefaultAsync();
            if (user == null)
                return Results.BadRequest();

            user.RefreshToken = null;
            await _context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
