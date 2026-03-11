using MindCarePro.Domain.Entities.BaseEntitie;
using MindCarePro.Domain.Entities.Users;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Domain.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        public string Title { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string Status { get; private set; }
        public string? BackgroundColor { get; private set; }
        public string? TextColor { get; private set; }
        public string? Display { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        // Construtor principal
        public Appointment(
            string title,
            DateTime start,
            DateTime end,
            string status,
            Guid userId,
            Guid patientId,
            string? backgroundColor = null,
            string? textColor = null,
            string? display = null
        )
        {
            Id = Guid.NewGuid();
            Title = title;
            Start = start;
            End = end;
            Status = status;
            UserId = userId;
            PatientId = patientId;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            Display = display;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }


        public void Delete(DateTime utcNow)
        {
            this.DeletedAt = utcNow;
        }

        
        public void UpdateStatus(string status, string? backgroundColor = null, string? textColor = null, string? display = null)
        {
            Status = status;
            if (backgroundColor != null) BackgroundColor = backgroundColor;
            if (textColor != null) TextColor = textColor;
            Display = display;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void SetVisualProperties(string backgroundColor, string textColor, string display)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            Display = display;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateUser(Guid userId)
        {
            UserId = userId;
        }
    }
}



