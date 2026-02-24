
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.Interfaces.Psycholgists;

public interface IPsychologistRepository
{
    Task Add(Psychologist psychologist);
    
    Task<Psychologist> GetById(Guid id);

    Task<IEnumerable<Psychologist>> GetAll();

    Task Update(Psychologist psychologist);

    Task Delete(Guid id);
}