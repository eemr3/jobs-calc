namespace JobsCalc.Communication.DTOs.Responses;

public class JobResponse
{
  public Guid JobId { get; set; }
  public string? Name { get; set; }
  public int DailyHours { get; set; }
  public int TotalHours { get; set; }
  public int RemainingDays { get; set; }
  public decimal ValueJob { get; set; }
  public bool Status { get; set; }
  public int UserId { get; set; }
}