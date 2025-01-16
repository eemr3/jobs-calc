namespace JobsCalc.Api.Http.Dtos;

public class JobDtoResponse
{
  public Guid JobId { get; set; }
  public string Name { get; set; } = null!;
  public int DailyHours { get; set; }
  public int TotalHours { get; set; }
  public int RemainingDays { get; set; }
  public decimal ValueJob { get; set; }
  public int UserId { get; set; }
}