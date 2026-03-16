using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Psychologists;

public class DeletePsychologistUseCase(
    IPsychologistRepository  psychologistRepository,
    ICurrentUser currentUser)
{
    private readonly IPsychologistRepository _psychologistRepository= psychologistRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Psychologist>> Execute(Guid id)
    {
        if (_currentUser.UserId is null)
        {
            return Result<Psychologist>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;

        var psychologist = await _psychologistRepository.GetById(id, userId);
        if ( psychologist== null)
        {
            return Result<Psychologist>.Failure(ResultErrorType.NotFound, "Psicólogo não encontrado");
        }
        
        psychologist.DeletedAt = DateTime.UtcNow;
        await _psychologistRepository.Update( psychologist);

        return Result<Psychologist>.Success(psychologist);
    }
}
