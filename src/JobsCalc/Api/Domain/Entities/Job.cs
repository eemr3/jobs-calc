using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobsCalc.Api.Domain.Entities;

public class Job
{
  public Guid JobId { get; set; }
  [Required]
  public string? Name { get; set; }
  public int DailyHours { get; set; }
  public int TotalHours { get; set; }
  public DateTime CreatedAt { get; set; }
  public int? UserId { get; set; }
  [JsonIgnore]
  public virtual User? User { get; set; }
}