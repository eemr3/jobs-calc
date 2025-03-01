using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobsCalc.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace JobsCalc.Common.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly int _expiresMinutes = 30;
  private readonly string _secretKey;

  public JwtTokenGenerator(string secretKey)
  {
    _secretKey = secretKey;
  }

  public string GenerateToken(int userId)
  {
    var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
    var credentials = new SigningCredentials(symmetric,
      SecurityAlgorithms.HmacSha256Signature);
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = AddClaims(userId),
      SigningCredentials = credentials,
      Expires = DateTime.UtcNow.AddMinutes(_expiresMinutes)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  private ClaimsIdentity AddClaims(int userId)
  {
    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));

    return claims;
  }
}