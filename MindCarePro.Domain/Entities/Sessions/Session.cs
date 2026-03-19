using MindCarePro.Domain.Entities.BaseEntitie;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Domain.Entities.Sessions;

public class Session: BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }
    public DateTime SessionDate { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string? BehavioralObservations { get; set; }
    public string? Interventions { get; set; }
    public string? PatientReactions { get; set; }
    public string? Referrals { get; set; }
    public string? TherapeuticPlans { get; set; }
    public string? DiagnosticHypotheses { get; set; }
    public string? TechniqueUsed { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.PENDING;
    
  
}
