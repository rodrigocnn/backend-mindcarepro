using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.Interfaces;

public interface IPatientRepository
{
    Task Add(Patient patient);
    
    Task<Patient?> GetById(Guid id);

    Task<IEnumerable<Patient>> GetAll();

    Task Update(Patient patient);

    Task Delete(Guid id);
}

