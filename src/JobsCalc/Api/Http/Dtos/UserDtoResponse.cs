namespace JobsCalc.Api.Http.Dtos;
public class UserDtoResponse
{
  public int UserId { get; set; }
  public string? FullName { get; set; }
  public string? Email { get; set; }
  public string? AvatarUrl { get; set; }
}