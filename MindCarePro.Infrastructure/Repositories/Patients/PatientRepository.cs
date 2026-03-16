using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Infrastructure.Persistence;

namespace MindCarePro.Infrastructure.Repositories.Patients;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    private readonly AppDbContext _context = context;

    public async Task Add(Patient category)
    {
        await _context.Patients.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetAll(Guid userId)
    {
        return await _context.Patients
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Patient?> GetById(Guid id)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Patient?> GetById(Guid id, Guid userId)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task Update(Patient category)
    {
        _context.Patients.Update(category);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
