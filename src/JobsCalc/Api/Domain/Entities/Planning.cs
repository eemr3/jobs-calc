using System.ComponentModel.DataAnnotations;

namespace JobsCalc.Api.Domain.Entities;

public class Planning
{
  public Guid PlanningId { get; set; }
  [Range(0, double.MaxValue, ErrorMessage = "O orçamento mensal deve ser um valor positivo")]
  public decimal MonthlyBbudget { get; set; }
  [Range(1, 7, ErrorMessage = "A quantidade de dias por semana deve estar entre 1 e 7.")]
  public int DaysPerWeek { get; set; }

  [Range(1, 24, ErrorMessage = "A quantidade de horas por dia deve estar entre 1 e 24.")]
  public int HoursPerDay { get; set; }
  [Range(1, 52, ErrorMessage = "A quantidade de semas por ano deve estar entre 1 e 52")]
  public int? VacationPerYear { get; set; }
  public decimal ValeuHour { get; set; }
  public int UserId { get; set; }
  public virtual User? User { get; set; }
}