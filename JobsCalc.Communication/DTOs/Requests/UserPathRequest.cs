namespace JobsCalc.Communication.DTOs.Requests;

public class UserPathRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Password { get; set; }
}