using System.ComponentModel.DataAnnotations;
using JobsCalc.Api.Application.Validators;

namespace JobsCalc.Api.Http.Dtos;

public class UserDtoRequest
{
  [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome completo deve ter entre 3 e 100 caracteres.")]
  public string FullName { get; set; } = null!;
  [Required(ErrorMessage = "O endereço de email é obrigatório.")]
  [EmailValidation(ErrorMessage = "O email dever ser um email válido")]
  public string Email { get; set; } = null!;
  [Required(ErrorMessage = "A senha é obrigatória")]
  [StringLength(30, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres e no máximo 30")]
  [PasswordValidation(ErrorMessage = "A senha deve incluir pelo menos uma letra minúscula, uma maíuscula, um dígito e um caractere especial (@, $, !, %, *, ?, &).")]
  public string Password { get; set; } = null!;
}