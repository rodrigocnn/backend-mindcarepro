using MindCarePro.Application.Dtos.Dashboards;
using MindCarePro.Application.Interfaces.Dashboards;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Dashboards;

public class GetDashboardUseCase(IDashboardRepository dashboardRepository, ICurrentUser currentUser)
{
    private readonly IDashboardRepository _dashboardRepository = dashboardRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<DashboardResponse>> Execute()
    {
        if (_currentUser.UserId is null)
        {
            return Result<DashboardResponse>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var now = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var nextMonthStart = monthStart.AddMonths(1);

        var patientsSummary = await _dashboardRepository.GetPatientsSummary(userId, monthStart, nextMonthStart);
        var appointmentsSummary = await _dashboardRepository.GetAppointmentsSummary(userId, monthStart, nextMonthStart);

        var chartNewPatients = await _dashboardRepository.GetNewPatientsChart(
            userId,
            startMonth: new DateTime(now.Year, 1, 1),
            months: 12);

        var chartAppointments = await _dashboardRepository.GetAppointmentsChart(
            userId,
            startMonth: monthStart.AddMonths(-4),
            months: 5);

        var response = new DashboardResponse(
            patients: patientsSummary,
            appointments: appointmentsSummary,
            chartNewPatients: chartNewPatients,
            chartAppointments: chartAppointments);

        return Result<DashboardResponse>.Success(response);
    }
}
