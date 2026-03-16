using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Shared;

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

    public async Task<Result<Patient>> Execute(Guid id, CreatePatientRequest request)
    {
        await _validationService.ValidateAsync(request);

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
        var patientMapped = _mapper.Map(request, patient);
        await _patientRepository.Update(patientMapped);

        return Result<Patient>.Success(patientMapped);
    }
}
