using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class DeletePsychologistUseCase(
    IPsychologistRepository  psychologistRepository,
    ICurrentUser currentUser)
{
    private readonly IPsychologistRepository _psychologistRepository= psychologistRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Psychologist> Execute(Guid id)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();

        var psychologist = await _psychologistRepository.GetById(id, userId);
        if ( psychologist== null)
        {
            throw new UnauthorizedAccessException();
        }
        
        psychologist.DeletedAt = DateTime.UtcNow;
        await _psychologistRepository.Update( psychologist);

        return psychologist;
    }
}
