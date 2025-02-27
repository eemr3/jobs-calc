namespace JobsCalc.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Planning? Planning { get; set; }
    public IEnumerable<Job>? Jobs { get; set; }
}