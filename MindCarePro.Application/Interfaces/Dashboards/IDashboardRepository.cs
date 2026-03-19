using MindCarePro.Application.Dtos.Dashboards;

namespace MindCarePro.Application.Interfaces.Dashboards;

public interface IDashboardRepository
{
    Task<PatientsSummaryResponse> GetPatientsSummary(Guid userId, DateTime monthStart, DateTime monthEnd);
    Task<AppointmentsSummary> GetAppointmentsSummary(Guid userId, DateTime monthStart, DateTime monthEnd);

    Task<IReadOnlyList<int>> GetNewPatientsChart(Guid userId, DateTime startMonth, int months);
    Task<IReadOnlyList<int>> GetAppointmentsChart(Guid userId, DateTime startMonth, int months);
}
