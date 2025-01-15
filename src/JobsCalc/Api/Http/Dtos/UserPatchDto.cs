namespace JobsCalc.Api.Http.Dtos;
public class UserPatchDto
{
  public string? FullName { get; set; }
  public string? Email { get; set; }
  public string? AvatarUrl { get; set; }
  public string? Password { get; set; }
}
