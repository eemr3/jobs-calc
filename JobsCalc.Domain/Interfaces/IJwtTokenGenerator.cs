namespace JobsCalc.Domain.Interfaces;

public interface IJwtTokenGenerator
{
  public string GenerateToken(int userId);
}