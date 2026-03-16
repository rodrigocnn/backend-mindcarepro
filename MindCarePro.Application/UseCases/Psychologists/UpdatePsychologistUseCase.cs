using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Domain.Shared;

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
    
    public async Task<Result<Psychologist>> Execute(Guid id, UpdatePsychologistRequest request)
    {
        await _validationService.ValidateAsync(request);
        if (_currentUser.UserId is null)
        {
            return Result<Psychologist>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;

        var psychologist = await _psychologistRepository.GetById(id, userId);
        if (psychologist == null)
        {
            return Result<Psychologist>.Failure(ResultErrorType.NotFound, "Psicólogo não encontrado");
        }

        var psychologistMapped = _mapper.Map(request, psychologist);
        await _psychologistRepository.Update(psychologistMapped);

        return Result<Psychologist>.Success(psychologistMapped);
    }

}
