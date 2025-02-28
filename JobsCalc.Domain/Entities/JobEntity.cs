using System.Text.Json.Serialization;

namespace JobsCalc.Domain.Entities;

public class JobEntity
{
    public Guid JobId { get; set; }
    public string Name { get; set; } = null!;
    public int DailyHours { get; set; }
    public int TotalHours { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}