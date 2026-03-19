using MindCarePro.Domain.Entities.Sessions;

namespace MindCarePro.Application.Interfaces.Sessions;

public interface ISessionRepository
{
    Task Add(Session session);
    Task<IEnumerable<Session>> GetAll(Guid userId);
    Task<IEnumerable<Session>> GetAllByPatient(Guid userId, Guid patientId);
    Task<Session?> GetById(Guid id, Guid userId);
    Task Update(Session session);
}
