using System.ComponentModel.DataAnnotations;

namespace JobsCalc.Api.Http.Dtos;

public class LoginRequest
{
  [Required(ErrorMessage = "Endereço de email é obrigatório.")]
  public string? Email { get; set; }
  public string? Password { get; set; }
}