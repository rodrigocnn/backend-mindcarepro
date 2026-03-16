using FluentValidation;
using MindCarePro.Application.Dtos.Auth;

namespace MindCarePro.Application.Validators.Users;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("Formato de e-mail inválido")
            .MaximumLength(200).WithMessage("O e-mail deve ter no máximo 200 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres")
            .MaximumLength(200).WithMessage("A senha deve ter no máximo 200 caracteres");
    }
}
