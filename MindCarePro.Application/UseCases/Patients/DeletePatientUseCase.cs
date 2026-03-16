using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class DeletePatientUseCase(
    IPatientRepository patientRepository,
    ICurrentUser currentUser)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Patient> Execute(Guid id)
    {

        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();

        var patient = await _patientRepository.GetById(id, userId);
        if (patient == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        patient.DeletedAt = DateTime.UtcNow;
        await _patientRepository.Update(patient);

        return patient;
    }
}
