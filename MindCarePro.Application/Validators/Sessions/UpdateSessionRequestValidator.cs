using FluentValidation;
using MindCarePro.Application.Dtos.Sessions;

namespace MindCarePro.Application.Validators.Sessions;

public class UpdateSessionRequestValidator : AbstractValidator<UpdateSessionRequest>
{
    public UpdateSessionRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("O ID do paciente é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ID do paciente é inválido.");

        RuleFor(x => x.SessionDate)
            .NotNull().WithMessage("A data da sessão é obrigatória.")
            .Must(date => date != null).WithMessage("A data da sessão fornecida é inválida.");

        RuleFor(x => x.Summary)
            .NotEmpty().WithMessage("O resumo é obrigatório.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("O status informado é inválido.");
    }
}
