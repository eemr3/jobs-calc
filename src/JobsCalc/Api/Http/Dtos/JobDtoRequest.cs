using System.ComponentModel.DataAnnotations;

namespace JobsCalc.Api.Http.Dtos;

public class JobDtoRequest
{
  [Required(ErrorMessage = "O nome do trabalho é obrigatório")]
  [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome dever terno mínimo 3 e no maxímo 50 caracteres")]
  public string Name { get; set; } = null!;

  [Required(ErrorMessage = "O valor da hora por dia é obrigatório")]
  [Range(1, 24, ErrorMessage = "O total dever ser maior que zero")]
  public int DailyHours { get; set; }

  [Required(ErrorMessage = "O total de horas é obrigatório")]
  [Range(1, int.MaxValue, ErrorMessage = "O total dever ser maior que zero")]
  public int TotalHours { get; set; }
}