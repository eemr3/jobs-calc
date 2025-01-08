using System.ComponentModel.DataAnnotations;
using JobsCalc.Api.Application.Validators;

namespace JobsCalc.Api.Domain.Entities;

public class User
{
  public int UserId { get; set; }
  [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome completo deve ter entre 3 e 100 caracteres.")]
  public string? FullName { get; set; }
  [Required(ErrorMessage = "O endereço de email é obrigatório.")]
  [EmailValidation(ErrorMessage = "O email dever ser um email válido")]
  public string? Email { get; set; }
  public string? PasswordHash { get; set; }
  public string? AvatarUrl { get; set; }
  public DateTime CreatedAt { get; set; }
  public ICollection<Jobs>? Jobs { get; set; }
}