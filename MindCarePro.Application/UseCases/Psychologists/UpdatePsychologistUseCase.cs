using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces.Psycholgists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class UpdatePsychologistUseCase(IPsychologistRepository  psychologistRepository, IMapper mapper)
{
    private readonly IPsychologistRepository _psychologistRepository= psychologistRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<PsychologistResponse> Execute(Guid id, UpdatePsychologistRequest request)
    {
  
        var patient = await  _psychologistRepository.GetById(id);
        if (patient == null)
        {
            throw new ArgumentNullException ("Psychologis not found"); 
        }
        var patientMapped = _mapper.Map(request, patient);
        await  _psychologistRepository.Update(patientMapped);
        
        return _mapper.Map<PsychologistResponse>(patientMapped);
    }

}