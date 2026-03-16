namespace MindCarePro.Application.Dtos.Appointments;

public class UpdateAppointmentRequest
{
    public string Title { get; set; } = string.Empty;
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public Guid PatientId { get; set; }
}
