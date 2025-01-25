using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JobsCalc.Api.Domain.Entities;

namespace JobsCalc.Api.Application.Services.JWT;

public class TokenGenerator
{
  private readonly int _expiresMinutes = 30;

  public string Generator(User user)
  {
    if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"))) throw new ArgumentNullException("JWT secret key is not configured.");

    var secret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!;

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = AddCalims(user),
      SigningCredentials = new SigningCredentials(symmetricSecurityKey,
          SecurityAlgorithms.HmacSha256Signature
      ),
      Expires = DateTime.UtcNow.AddMinutes(_expiresMinutes)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  private static ClaimsIdentity AddCalims(User user)
  {
    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
    claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
    claims.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName));
    claims.AddClaim(new Claim("avatar_url", user.AvatarUrl ?? ""));

    return claims;
  }
}