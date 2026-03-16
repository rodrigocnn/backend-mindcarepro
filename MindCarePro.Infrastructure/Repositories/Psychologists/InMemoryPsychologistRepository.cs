using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Infrastructure.Repositories.Psychologists;

public class InMemoryPsychologistRepository : IPsychologistRepository
{
    private readonly List<Psychologist> _psychologists = new();

    public Task Add(Psychologist psychologist)
    {
        _psychologists.Add(psychologist);
        return Task.CompletedTask;
    }

    public Task<Psychologist?> GetById(Guid id)
    {
        var psychologist = _psychologists.FirstOrDefault(p => p.Id == id && p.DeletedAt == null);
        return Task.FromResult(psychologist);
    }

    public Task<Psychologist?> GetById(Guid id, Guid userId)
    {
        var psychologist = _psychologists.FirstOrDefault(p =>
            p.Id == id && p.Id == userId && p.DeletedAt == null);
        return Task.FromResult(psychologist);
    }

    public Task<IEnumerable<Psychologist>> GetAll()
    {
        var all = _psychologists.Where(p => p.DeletedAt == null);
        return Task.FromResult(all);
    }

    public Task<IEnumerable<Psychologist>> GetAll(Guid userId)
    {
        var all = _psychologists.Where(p => p.Id == userId && p.DeletedAt == null);
        return Task.FromResult(all);
    }

    public Task Update(Psychologist psychologist)
    {
        var index = _psychologists.FindIndex(p => p.Id == psychologist.Id);
        if (index >= 0)
        {
            _psychologists[index] = psychologist;
        }
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var psychologist = _psychologists.FirstOrDefault(p => p.Id == id);
        if (psychologist != null)
        {
            psychologist.DeletedAt = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}
