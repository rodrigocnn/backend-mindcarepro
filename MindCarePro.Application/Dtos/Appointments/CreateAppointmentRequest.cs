using System;

namespace MindCarePro.Application.Dtos.Appointments
{
    public class CreateAppointmentRequest
    {
        public string Title { get; set; } = string.Empty; 
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid UserId { get; set; } 
        public Guid PatientId { get; set; }

    }
}