using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace Api.Infrasrtucture
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
