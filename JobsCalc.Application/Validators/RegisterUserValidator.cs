using FluentValidation;
using JobsCalc.Communication.DTOs.Requests;

namespace JobsCalc.Application.Validators;

public class RegisterUserValidator : AbstractValidator<UserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.FullName).NotEmpty().WithMessage("O nome é obrigatorio")
            .Length(3, 100).WithMessage("O nome deve ter no mínimo 3 caracteres e no máximo 100 caracteres");
        RuleFor(request => request.Email).NotEmpty().WithMessage("Informe o endereço de e-mail")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("O endereço de e-mail é inválido");
        RuleFor(request => request.Password).NotEmpty().WithMessage("A senha é obrigatoria");
        RuleFor(request => request.Password).MinimumLength(6).WithMessage("Sua senha precisa ter no mínimo 6 caracteres.");
        RuleFor(request => request.Password).MaximumLength(32).WithMessage("Sua senha não pode exceder a 32 caracteres.");
        RuleFor(request => request.Password).Matches(@"[A-Z]+").WithMessage("Sua senha deve conter pelo menos uma letra maiúscula.");
        RuleFor(request => request.Password).Matches(@"[a-z]+").WithMessage("Sua senha deve conter pelo menos uma letra minúscula.");
        RuleFor(request => request.Password).Matches(@"[0-9]+").WithMessage("Sua senha deve conter pelo menos um número.");
        RuleFor(request => request.Password).Matches(@"[\@\!\?\*\.]+").WithMessage("Sua senha deve conter pelo menos um (@!? *.).");
    }
}