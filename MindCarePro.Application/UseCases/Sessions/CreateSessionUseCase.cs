using MindCarePro.Application.Dtos.Sessions;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Sessions;

public class CreateSessionUseCase(
    ISessionRepository sessionRepository,
    IValidationService validationService,
    ICurrentUser currentUser)
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Session>> Execute(CreateSessionRequest request)
    {
        await _validationService.ValidateAsync(request);

        if (_currentUser.UserId is null)
        {
            return Result<Session>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var session = new Session
        {
            UserId = _currentUser.UserId.Value,
            PatientId = request.PatientId,
            SessionDate = request.SessionDate,
            Summary = request.Summary,
            BehavioralObservations = request.BehavioralObservations,
            Interventions = request.Interventions,
            PatientReactions = request.PatientReactions,
            Referrals = request.Referrals,
            TherapeuticPlans = request.TherapeuticPlans,
            DiagnosticHypotheses = request.DiagnosticHypotheses,
            TechniqueUsed = request.TechniqueUsed
        };

        await _sessionRepository.Add(session);
        return Result<Session>.Success(session);
    }
}
