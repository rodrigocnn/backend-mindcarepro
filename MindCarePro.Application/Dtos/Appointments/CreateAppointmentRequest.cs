using System;

namespace MindCarePro.Application.Dtos.Appointments
{
    public class CreateAppointmentRequest
    {
   
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid PatientId { get; set; }

    }
}
