using FluentValidation;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Domain.Utils;

namespace MindCarePro.Application.Validators.Patients;

public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
{
    public CreatePatientRequestValidator()
    {
        RuleFor(patient => patient.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres");

        RuleFor(patient => patient.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("Formato de e-mail inválido")
            .MaximumLength(200).WithMessage("O e-mail deve ter no máximo 200 caracteres");

        RuleFor(p => p.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório")
            .Must(cpf =>
            {
                try
                {
                    return CpfValidator.Validate(cpf);
                }
                catch
                {
                    return false;
                }
            })
            .WithMessage("CPF inválido");

        RuleFor(patient => patient.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .MaximumLength(20).WithMessage("O telefone deve ter no máximo 20 caracteres");

        RuleFor(patient => patient.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória")
            .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser no passado");

        RuleFor(patient => patient.Rg)
            .NotEmpty().WithMessage("O RG é obrigatório")
            .MaximumLength(20).WithMessage("O RG deve ter no máximo 20 caracteres");

        RuleFor(patient => patient.Gender)
            .NotEmpty().WithMessage("O gênero é obrigatório")
            .MaximumLength(30).WithMessage("O gênero deve ter no máximo 30 caracteres");

        RuleFor(patient => patient.Notes)
            .MaximumLength(1000).WithMessage("As observações devem ter no máximo 1000 caracteres");
    }
}