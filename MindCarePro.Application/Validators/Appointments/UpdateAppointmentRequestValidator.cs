using FluentValidation;
using MindCarePro.Application.Dtos.Appointments;

namespace MindCarePro.Application.Validators.Appointments;

public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentRequestValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("O status é obrigatório.")
            .MaximumLength(150).WithMessage("O status deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Start)
            .NotNull().WithMessage("A data de início é obrigatória.")
            .Must(date => date != null).WithMessage("A data de início fornecida é inválida.");

        RuleFor(x => x.End)
            .NotNull().WithMessage("A data de término é obrigatória.")
            .Must(date => date != null).WithMessage("A data de término fornecida é inválida.")
            .GreaterThan(x => x.Start).WithMessage("A data de término deve ser maior que a data de início.");
        
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("O ID do paciente é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ID do paciente é inválido.");
    }
}
