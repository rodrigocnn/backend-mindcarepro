using AutoMapper;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class CreatePsychologistUseCase(
    IPsychologistRepository psychologistRepository,
    IValidationService validationService,   
    IPasswordEncripter  passwordEncripter,
    IMapper mapper)
{
    private readonly  IPsychologistRepository _psychologistRepository = psychologistRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidationService _validationService = validationService;
    private readonly IPasswordEncripter _passwordEncripter =  passwordEncripter;

    public async Task<Psychologist> Execute(CreatePsychologistRequest request)
    {
        await _validationService.ValidateAsync(request);
        var psychologist = _mapper.Map<Psychologist>(request);
        psychologist.Password = _passwordEncripter.Encrypt(psychologist.Password);
        
        await  _psychologistRepository.Add(psychologist);
        return psychologist;
    }
}