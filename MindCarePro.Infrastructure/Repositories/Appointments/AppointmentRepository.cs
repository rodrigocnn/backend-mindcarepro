using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Enums;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Infrastructure.Persistence;

namespace MindCarePro.Infrastructure.Repositories.Appointments;

public class AppointmentRepository(AppDbContext context) : IAppointmentRepository
{
    private readonly AppDbContext _context = context;

    public async Task Add(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _context.Appointments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAll(Guid userId)
    {
        return await _context.Appointments
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> HasOverlap(Guid userId, DateTime startDate, DateTime endDate, Guid? excludeAppointmentId = null)
    {
        return await _context.Appointments
            .AsNoTracking()
            .AnyAsync(a =>
                a.UserId == userId &&
                a.Status != AppointmentStatus.Canceled &&
                a.Start < endDate &&
                a.End > startDate &&
                (excludeAppointmentId == null || a.Id != excludeAppointmentId));
    }

    public async Task<Appointment?> GetById(Guid id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Appointment?> GetById(Guid id, Guid userId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task Update(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public Task Delete(DateTime id)
    {
        throw new NotImplementedException();
    }
}
