
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.Interfaces.Psycholgists;

public interface IPsychologistRepository
{
    Task Add(Psychologist psychologist);
    
    Task<Psychologist> GetById(Guid id);
    Task<Psychologist?> GetById(Guid id, Guid userId);

    Task<IEnumerable<Psychologist>> GetAll();
    Task<IEnumerable<Psychologist>> GetAll(Guid userId);

    Task Update(Psychologist psychologist);

    Task Delete(Guid id);
}
