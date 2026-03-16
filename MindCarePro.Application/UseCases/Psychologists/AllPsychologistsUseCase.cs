using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class AllPsychologistsUseCase(
    IPsychologistRepository psychologistRepository,
    ICurrentUser currentUser)
{
    private readonly IPsychologistRepository _psychologistRepository =  psychologistRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    
    public async Task<IEnumerable<Psychologist>>Execute()
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();
        return await _psychologistRepository.GetAll(userId);
    }

}
