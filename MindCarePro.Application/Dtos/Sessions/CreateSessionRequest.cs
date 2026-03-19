namespace MindCarePro.Application.Dtos.Sessions;

public class CreateSessionRequest
{
    public Guid PatientId { get; set; }
    public DateTime SessionDate { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string? BehavioralObservations { get; set; }
    public string? Interventions { get; set; }
    public string? PatientReactions { get; set; }
    public string? Referrals { get; set; }
    public string? TherapeuticPlans { get; set; }
    public string? DiagnosticHypotheses { get; set; }
    public string? TechniqueUsed { get; set; }
}
