using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Patients;

public class CreatePatientUseCase(
    IPatientRepository patientRepository,
    IValidationService validationService,   
    ICurrentUser currentUser,
    IMapper mapper)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Patient>> Execute(CreatePatientRequest request)
    {
        await _validationService.ValidateAsync(request);
        
        var userId = _currentUser.UserId ?? null;

        if (userId == null)
        {
            return Result<Patient>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }
     
        var patient = _mapper.Map<Patient>(request);
        patient.UpdateUser(userId.Value);
        await _patientRepository.Add(patient);
        return  Result<Patient>.Success(patient);
       
    }
}


