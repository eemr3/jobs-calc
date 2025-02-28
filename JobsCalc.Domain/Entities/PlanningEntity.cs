using System.Text.Json.Serialization;

namespace JobsCalc.Domain.Entities;

public class PlanningEntity
{
    public Guid PlanningId { get; set; }
    public decimal MonthlyBudget { get; set; }
    public int DaysPerWeek { get; set; }
    public int HoursPerDay { get; set; }
    public int? VacationPerYear { get; set; }
    public decimal ValueHour { get; set; }
    public int? UserId { get; set; }
    
    [JsonIgnore]
    public UserEntity? User { get; set; }
}