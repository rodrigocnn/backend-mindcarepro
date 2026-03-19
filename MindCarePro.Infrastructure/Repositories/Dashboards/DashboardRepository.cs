using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Dtos.Dashboards;
using MindCarePro.Application.Enums;
using MindCarePro.Application.Interfaces.Dashboards;
using MindCarePro.Infrastructure.Persistence;

namespace MindCarePro.Infrastructure.Repositories.Dashboards;

public class DashboardRepository(AppDbContext context) : IDashboardRepository
{
    private readonly AppDbContext _context = context;

    public async Task<PatientsSummaryResponse> GetPatientsSummary(Guid userId, DateTime monthStart, DateTime monthEnd)
    {
        var query = _context.Patients
            .AsNoTracking()
            .Where(p => p.UserId == userId);

        var total = await query.CountAsync();
        var totalCurrentMonth = await query
            .Where(p => p.CreatedAt >= monthStart && p.CreatedAt < monthEnd)
            .CountAsync();

        return new PatientsSummaryResponse(total, totalCurrentMonth);
    }

    public async Task<AppointmentsSummary> GetAppointmentsSummary(Guid userId, DateTime monthStart, DateTime monthEnd)
    {
        var query = _context.Appointments
            .AsNoTracking()
            .Where(a => a.UserId == userId);

        var total = await query.CountAsync();
        var totalCurrentMonth = await query
            .Where(a => a.Start >= monthStart && a.Start < monthEnd)
            .CountAsync();

        var totalCompleted = await query
            .Where(a => a.Status == AppointmentStatus.Completed)
            .CountAsync();
        var totalCompletedCurrentMonth = await query
            .Where(a => a.Status == AppointmentStatus.Completed && a.Start >= monthStart && a.Start < monthEnd)
            .CountAsync();

        var totalCanceled = await query
            .Where(a => a.Status == AppointmentStatus.Canceled)
            .CountAsync();
        var totalCanceledCurrentMonth = await query
            .Where(a => a.Status == AppointmentStatus.Canceled && a.Start >= monthStart && a.Start < monthEnd)
            .CountAsync();

        return new AppointmentsSummary(
            total,
            totalCurrentMonth,
            totalCompleted,
            totalCompletedCurrentMonth,
            totalCanceled,
            totalCanceledCurrentMonth);
    }

    public async Task<IReadOnlyList<int>> GetNewPatientsChart(Guid userId, DateTime startMonth, int months)
    {
        var endMonth = startMonth.AddMonths(months);

        var data = await _context.Patients
            .AsNoTracking()
            .Where(p => p.UserId == userId && p.CreatedAt >= startMonth && p.CreatedAt < endMonth)
            .GroupBy(p => new { p.CreatedAt.Year, p.CreatedAt.Month })
            .Select(g => new MonthCount(g.Key.Year, g.Key.Month, g.Count()))
            .ToListAsync();

        return BuildMonthlySeries(startMonth, months, data);
    }

    public async Task<IReadOnlyList<int>> GetAppointmentsChart(Guid userId, DateTime startMonth, int months)
    {
        var endMonth = startMonth.AddMonths(months);

        var data = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.UserId == userId && a.Start >= startMonth && a.Start < endMonth)
            .GroupBy(a => new { a.Start.Year, a.Start.Month })
            .Select(g => new MonthCount(g.Key.Year, g.Key.Month, g.Count()))
            .ToListAsync();

        return BuildMonthlySeries(startMonth, months, data);
    }

    private static IReadOnlyList<int> BuildMonthlySeries(
        DateTime startMonth,
        int months,
        IEnumerable<MonthCount> data)
    {
        var counts = data.ToDictionary(
            item => (item.Year, item.Month),
            item => item.Count);

        var result = new List<int>(months);
        for (var i = 0; i < months; i++)
        {
            var current = startMonth.AddMonths(i);
            result.Add(counts.TryGetValue((current.Year, current.Month), out var count) ? count : 0);
        }

        return result;
    }

    private sealed record MonthCount(int Year, int Month, int Count);
}
