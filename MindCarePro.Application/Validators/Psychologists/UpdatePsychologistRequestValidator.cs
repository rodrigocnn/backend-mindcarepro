using FluentValidation;

using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Domain.Utils;

namespace MindCarePro.Application.Validators.Psychologists;

public class UpdatePsychologistRequestValidator : AbstractValidator<UpdatePsychologistRequest>
{

    public UpdatePsychologistRequestValidator()
    {
        RuleFor(psychologist => psychologist.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres");
        
        RuleFor(psychologist => psychologist.Email)
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
        
        RuleFor(psychologist => psychologist.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .MaximumLength(20).WithMessage("O telefone deve ter no máximo 20 caracteres");

        RuleFor(psychologist => psychologist.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória")
            .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser no passado");
        
        RuleFor(psychologist => psychologist.Rg)
            .NotEmpty().WithMessage("O RG é obrigatório")
            .MaximumLength(20).WithMessage("O RG deve ter no máximo 20 caracteres");

        RuleFor(psychologist => psychologist.Crp)
            .NotEmpty().WithMessage("O CRP é obrigatório");
    } 
}
