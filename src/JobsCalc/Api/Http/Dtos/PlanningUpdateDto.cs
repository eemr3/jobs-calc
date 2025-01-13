namespace JobsCalc.Api.Http.Dtos;

public class PlanningUpdateDto
{
  public Guid PlanningId { get; set; }
  public decimal MonthlyBudget { get; set; }
  public int DaysPerWeek { get; set; }
  public int HoursPerDay { get; set; }
  public int? VacationPerYear { get; set; }
  public int? UserId { get; set; }
}