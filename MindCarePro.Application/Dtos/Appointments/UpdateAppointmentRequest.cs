namespace MindCarePro.Application.Dtos.Appointments;

public class UpdateAppointmentRequest
{

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid? PatientId { get; set; }
}
