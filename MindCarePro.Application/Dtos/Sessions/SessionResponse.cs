using MindCarePro.Domain.Entities.Sessions;

namespace MindCarePro.Application.Dtos.Sessions;

public class SessionResponse(
    Guid id,
    Guid userId,
    Guid patientId,
    DateTime sessionDate,
    string summary,
    string? behavioralObservations,
    string? interventions,
    string? patientReactions,
    string? referrals,
    string? therapeuticPlans,
    string? diagnosticHypotheses,
    string? techniqueUsed,
    SessionStatus status,
    DateTime createdAt,
    DateTime updatedAt,
    DateTime? deletedAt
)
{
    public Guid Id { get; } = id;
    public Guid UserId { get; } = userId;
    public Guid PatientId { get; } = patientId;
    public DateTime SessionDate { get; } = sessionDate;
    public string Summary { get; } = summary;
    public string? BehavioralObservations { get; } = behavioralObservations;
    public string? Interventions { get; } = interventions;
    public string? PatientReactions { get; } = patientReactions;
    public string? Referrals { get; } = referrals;
    public string? TherapeuticPlans { get; } = therapeuticPlans;
    public string? DiagnosticHypotheses { get; } = diagnosticHypotheses;
    public string? TechniqueUsed { get; } = techniqueUsed;
    public SessionStatus Status { get; } = status;
    public DateTime CreatedAt { get; } = createdAt;
    public DateTime UpdatedAt { get; } = updatedAt;
    public DateTime? DeletedAt { get; } = deletedAt;
}
