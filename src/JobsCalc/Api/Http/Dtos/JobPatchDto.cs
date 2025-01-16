namespace JobsCalc.Api.Http.Dtos;

public class JobPatchDto
{
  public string? Name { get; set; }
  public int? DailyHours { get; set; }
  public int? TotalHours { get; set; }
}