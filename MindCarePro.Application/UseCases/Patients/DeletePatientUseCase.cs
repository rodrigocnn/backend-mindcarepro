using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class DeletePatientUseCase(IPatientRepository patientRepository)
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<Patient> Execute(Guid id)
    {

        var patient = await _patientRepository.GetById(id);
        if (patient == null)
        {
            throw new Exception($"Patient with id {id} not found."); // ou NotFoundException
        }
        
        patient.DeletedAt = DateTime.UtcNow;
        await _patientRepository.Update(patient);

        return patient;
    }
}