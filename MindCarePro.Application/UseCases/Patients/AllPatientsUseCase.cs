using Microsoft.AspNetCore.Http;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class AllPatientsUseCase(IPatientRepository patientRepository, ICurrentUser currentUser)
{
    
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    
    public async Task<IEnumerable<Patient>>Execute()
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();
        return await _patientRepository.GetAll(userId);
    }

}