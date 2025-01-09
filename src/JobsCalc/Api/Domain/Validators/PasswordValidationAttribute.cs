using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JobsCalc.Api.Application.Validators;

public class PasswordValidationAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    if (value == null) return false;

    var password = value.ToString()!;

    return Regex.IsMatch(password, @"((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
  }
}