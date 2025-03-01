namespace JobsCalc.Communication.DTOs.Requests;

public class PlanningRequest
{
  public decimal MonthlyBudget { get; set; }
  public int DaysPerWeek { get; set; }
  public int HoursPerDay { get; set; }
  public int VacationPerYear { get; set; }
  public int? UserId { get; set; }
}