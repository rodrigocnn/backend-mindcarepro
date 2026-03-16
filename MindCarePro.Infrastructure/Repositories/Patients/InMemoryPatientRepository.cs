using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Infrastructure.Repositories.Patients;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients = new();

    public Task Add(Patient patient)
    {
        _patients.Add(patient);
        return Task.CompletedTask;
    }

    public Task<Patient?> GetById(Guid id)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == id && p.DeletedAt == null);
        return Task.FromResult(patient);
    }

    public Task<Patient?> GetById(Guid id, Guid userId)
    {
        var patient = _patients.FirstOrDefault(p =>
            p.Id == id && p.UserId == userId && p.DeletedAt == null);
        return Task.FromResult(patient);
    }

    public Task<IEnumerable<Patient>> GetAll(Guid userId)
    {
        var all = _patients
            .Where(p => p.DeletedAt == null && p.UserId == userId);
        return Task.FromResult(all);
    }

    public Task Update(Patient patient)
    {
        var index = _patients.FindIndex(p => p.Id == patient.Id);
        if (index >= 0)
        {
            _patients[index] = patient;
        }
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == id);
        if (patient != null)
        {
            patient.DeletedAt = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}
