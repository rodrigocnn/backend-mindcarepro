using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.Interfaces;

public interface IPatientRepository
{
    Task Add(Patient patient);
    
    Task<Patient?> GetById(Guid id);

    Task<IEnumerable<Patient>> GetAll(Guid id);

    Task Update(Patient patient);

    Task Delete(Guid id);
}

