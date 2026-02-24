using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class CreatePatientUseCase(
    IPatientRepository patientRepository,
    IValidationService validationService,   
    IMapper mapper)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidationService _validationService = validationService;

    public async Task<Patient> Execute(CreatePatientRequest request)
    {
        await _validationService.ValidateAsync(request);

        var patient = _mapper.Map<Patient>(request);
        await _patientRepository.Add(patient);

        return patient;
    }
}


