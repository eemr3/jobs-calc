using System.Text.Json.Serialization;

namespace JobsCalc.Communication.DTOs.Requests;

public class JobRequest
{
  public string Name { get; set; } = null!;
  public int DailyHours { get; set; }
  public int TotalHours { get; set; }
  
  [JsonIgnore]
  public int UserId { get; set; }
}