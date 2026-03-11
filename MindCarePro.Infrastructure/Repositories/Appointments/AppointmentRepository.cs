using Microsoft.EntityFrameworkCore;

using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Entities.Patients;
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

    
    public async Task<Appointment?> GetById(Guid id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
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