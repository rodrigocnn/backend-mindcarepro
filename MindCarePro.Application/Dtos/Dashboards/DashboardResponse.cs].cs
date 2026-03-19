namespace MindCarePro.Application.Dtos.Dashboards;

public class DashboardResponse(
    PatientsSummaryResponse patients,
    AppointmentsSummary appointments,
    IReadOnlyList<int> chartNewPatients,
    IReadOnlyList<int> chartAppointments)
{
    public PatientsSummaryResponse Patients { get; } = patients;
    public AppointmentsSummary Appointments { get; } = appointments;
    public IReadOnlyList<int> ChartNewPatients { get; } = chartNewPatients;
    public IReadOnlyList<int> ChartAppointments { get; } = chartAppointments;
}