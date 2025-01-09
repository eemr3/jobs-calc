using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JobsCalc.Api.Application.Validators;

public class EmailValidationAttribute : ValidationAttribute
{

  public override bool IsValid(object? value)
  {
    if (value == null) return false;

    var email = value?.ToString()!;

    return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
  }
}