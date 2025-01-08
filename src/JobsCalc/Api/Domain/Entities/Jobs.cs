using System.ComponentModel.DataAnnotations;

namespace JobsCalc.Api.Domain.Entities;

public class Jobs
{
  public Guid JobId { get; set; }
  [Required(ErrorMessage = "O nome do trabalho é obrigatório")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do trabalho deve ter entre 3 e 100 caracteres.")]
  public string? Name { get; set; }
  [Range(1, 24, ErrorMessage = "A quantidade de horas por dia tem que estar entre 1 e 24")]
  public int DailyHours { get; set; }
  [Range(1, int.MaxValue, ErrorMessage = "O total de horas que pretende trabalhar não pode ser negativo")]
  public int TotalHours { get; set; }
  public DateTime CreatedAt { get; set; }
  public int UserId { get; set; }
  public virtual User? Users { get; set; }
}