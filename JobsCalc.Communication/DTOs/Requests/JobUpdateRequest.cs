namespace JobsCalc.Communication.DTOs.Requests;

public class JobUpdateRequest
{
  public string? Name { get; set; }
  public int? DailyHours { get; set; }
  public int? TotalHours { get; set; }
}