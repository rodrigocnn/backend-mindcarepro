using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;

namespace MindCarePro.Application.UseCases.Psychologists;

public class UpdatePsychologistUseCase(
    IPsychologistRepository  psychologistRepository,
    IValidationService validationService,
    ICurrentUser currentUser,
    IMapper mapper)
{
    private readonly IPsychologistRepository _psychologistRepository= psychologistRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IMapper _mapper = mapper;
    
    public async Task<PsychologistResponse> Execute(Guid id, UpdatePsychologistRequest request)
    {
        await _validationService.ValidateAsync(request);
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();

        var patient = await  _psychologistRepository.GetById(id, userId);
        if (patient == null)
        {
            throw new UnauthorizedAccessException();
        }
        var patientMapped = _mapper.Map(request, patient);
        await  _psychologistRepository.Update(patientMapped);
        
        return _mapper.Map<PsychologistResponse>(patientMapped);
    }

}
