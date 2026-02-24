using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class AllPatientsUseCase(IPatientRepository patientRepository)
{
    
    private readonly IPatientRepository _patientRepository = patientRepository;
    
    public async Task<IEnumerable<Patient>>Execute()
    {
        return await _patientRepository.GetAll();
    }

}