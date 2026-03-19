using AutoMapper;
using MindCarePro.Application.Dtos.Sessions;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Sessions;

public class UpdateSessionUseCase(
    ISessionRepository sessionRepository,
    IValidationService validationService,
    ICurrentUser currentUser,
    IMapper mapper)
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Session>> Execute(Guid id, UpdateSessionRequest request)
    {
        await _validationService.ValidateAsync(request);

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

        _mapper.Map(request, session);
        await _sessionRepository.Update(session);
        return Result<Session>.Success(session);
    }
}
