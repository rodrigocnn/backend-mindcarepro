using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Infrastructure.Persistence;

namespace MindCarePro.Infrastructure.Repositories.Sessions;

public class SessionRepository(AppDbContext context) : ISessionRepository
{
    private readonly AppDbContext _context = context;

    public async Task Add(Session session)
    {
        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Session>> GetAll(Guid userId)
    {
        return await _context.Sessions
            .Where(s => s.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllByPatient(Guid userId, Guid patientId)
    {
        return await _context.Sessions
            .Where(s => s.UserId == userId && s.PatientId == patientId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Session?> GetById(Guid id, Guid userId)
    {
        return await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
    }

    public async Task Update(Session session)
    {
        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();
    }
}
