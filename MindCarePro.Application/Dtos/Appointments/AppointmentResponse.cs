using System;

namespace MindCarePro.Application.Dtos.Appointments;

public class AppointmentResponse(
    Guid id,
    string title,
    DateTime start,
    DateTime end,
    string status,
    string? backgroundColor,
    string? textColor,
    string? display,
    Guid userId,
    Guid patientId,
    DateTime createdAt,
    DateTime updatedAt,
    DateTime? deletedAt
)
{
    public Guid Id { get; } = id;
    public string Title { get; } = title;
    public DateTime Start { get; } = start;
    public DateTime End { get; } = end;
    public string Status { get; } = status;
    public string? BackgroundColor { get; } = backgroundColor;
    public string? TextColor { get; } = textColor;
    public string? Display { get; } = display;
    public Guid UserId { get; } = userId;
    public Guid PatientId { get; } = patientId;
    public DateTime CreatedAt { get; } = createdAt;
    public DateTime UpdatedAt { get; } = updatedAt;
    public DateTime? DeletedAt { get; } = deletedAt;
}