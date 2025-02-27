using System.Text.Json.Serialization;

namespace JobsCalc.Domain.Entities;

public class Job
{
    public Guid JobId { get; set; }
    public string Name { get; set; } = null!;
    public int DailyHours { get; set; }
    public int TotalHours { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}