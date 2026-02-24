
using Microsoft.EntityFrameworkCore;
using MindCarePro.Application.Interfaces.Psycholgists;

using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Infrastructure.Persistence;

namespace MindCarePro.Infrastructure.Repositories.Psychologists;

public class PsychologistRepository(AppDbContext context) : IPsychologistRepository
{
    private readonly AppDbContext _context = context;

    public async Task Add(Psychologist psychologist)
    {
        await _context.Psychologists.AddAsync(psychologist);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Psychologist>> GetAll()
    {
        return await _context.Psychologists
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Psychologist?> GetById(Guid id)
    {
        return await _context.Psychologists
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Update(Psychologist psychologist)
    {
        _context.Psychologists.Update(psychologist);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}