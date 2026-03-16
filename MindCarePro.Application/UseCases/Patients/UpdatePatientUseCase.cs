using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class UpdatePatientUseCase(
    IPatientRepository patientRepository,
    IValidationService validationService,
    ICurrentUser currentUser,
    IMapper mapper)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IMapper _mapper = mapper;

    public async Task<PatientResponse> Execute(Guid id, CreatePatientRequest request)
    {
        await _validationService.ValidateAsync(request);

        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();

        var patient = await _patientRepository.GetById(id, userId);
        if (patient == null)
        {
            throw new UnauthorizedAccessException();
        }
        var patientMapped = _mapper.Map(request, patient);
        await _patientRepository.Update(patientMapped);
        
        return _mapper.Map<PatientResponse>(patientMapped);
    }
}
