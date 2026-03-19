namespace MindCarePro.Application.Dtos.Dashboards;

public class PatientsSummaryResponse(int total, int totalCurrentMonth)
{
    public int Total { get; } = total;
    public int TotalCurrentMonth { get; } = totalCurrentMonth;

}