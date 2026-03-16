using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Patients;

public class DeletePatientUseCase(
    IPatientRepository patientRepository,
    ICurrentUser currentUser)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Patient>> Execute(Guid id)
    {

        if (_currentUser.UserId is null)
        {
            return Result<Patient>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;

        var patient = await _patientRepository.GetById(id, userId);
        if (patient == null)
        {
            return Result<Patient>.Failure(ResultErrorType.NotFound, "Paciente não encontrado");
        }
        
        patient.DeletedAt = DateTime.UtcNow;
        await _patientRepository.Update(patient);

        return Result<Patient>.Success(patient);
    }
}
