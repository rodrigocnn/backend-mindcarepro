namespace MindCarePro.Application.Dtos.Dashboards;

public class AppointmentsSummary(
    int total,
    int totalCurrentMonth,
    int totalCompleted,
    int totalCompletedCurrentMonth,
    int totalCanceled,
    int totalCanceledCurrentMonth)
{
    public int Total { get; } = total;
    public int TotalCurrentMonth { get; } = totalCurrentMonth;
    public int TotalCompleted { get; } = totalCompleted;
    public int TotalCompletedCurrentMonth { get; } = totalCompletedCurrentMonth;
    public int TotalCanceled { get; } = totalCanceled;
    public int TotalCanceledCurrentMonth { get; } = totalCanceledCurrentMonth;
}