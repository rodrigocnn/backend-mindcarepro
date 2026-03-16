using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Psychologists;

public class AllPsychologistsUseCase(
    IPsychologistRepository psychologistRepository,
    ICurrentUser currentUser)
{
    private readonly IPsychologistRepository _psychologistRepository =  psychologistRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    
    public async Task<Result<IEnumerable<Psychologist>>> Execute()
    {
        if (_currentUser.UserId is null)
        {
            return Result<IEnumerable<Psychologist>>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var psychologists = await _psychologistRepository.GetAll(userId);
        return Result<IEnumerable<Psychologist>>.Success(psychologists);
    }

}
