namespace JobsCalc.Domain.Interfaces.Services;

public interface IJwtTokenGenerator
{
  public string GenerateToken(int userId);
}