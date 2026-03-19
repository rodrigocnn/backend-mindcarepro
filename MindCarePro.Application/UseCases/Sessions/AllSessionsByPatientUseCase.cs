using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Sessions;

public class AllSessionsByPatientUseCase(
    ISessionRepository sessionRepository,
    ICurrentUser currentUser)
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<IEnumerable<Session>>> Execute(Guid patientId)
    {
        if (_currentUser.UserId is null)
        {
            return Result<IEnumerable<Session>>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var sessions = await _sessionRepository.GetAllByPatient(userId, patientId);
        return Result<IEnumerable<Session>>.Success(sessions);
    }
}
