using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Sessions;

public class ShowSessionUseCase(
    ISessionRepository sessionRepository,
    ICurrentUser currentUser)
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Session>> Execute(Guid id)
    {
        if (_currentUser.UserId is null)
        {
            return Result<Session>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var session = await _sessionRepository.GetById(id, userId);

        if (session is null)
        {
            return Result<Session>.Failure(ResultErrorType.NotFound, "Sessão não encontrada");
        }

        return Result<Session>.Success(session);
    }
}
