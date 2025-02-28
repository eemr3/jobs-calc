namespace JobsCalc.Domain.Entities;

public class UserEntity
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public PlanningEntity? Planning { get; set; }
    public IEnumerable<JobEntity>? Jobs { get; set; }
}