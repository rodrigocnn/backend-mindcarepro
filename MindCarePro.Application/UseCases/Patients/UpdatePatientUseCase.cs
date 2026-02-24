using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.UseCases.Patients;

public class UpdatePatientUseCase(IPatientRepository patientRepository, IMapper mapper)
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PatientResponse> Execute(Guid id, CreatePatientRequest request)
    {
  
        var patient = await _patientRepository.GetById(id);
        if (patient == null)
        {
            throw new ArgumentNullException ("Patient not found"); 
        }
        var patientMapped = _mapper.Map(request, patient);
        await _patientRepository.Update(patientMapped);
        
        return _mapper.Map<PatientResponse>(patientMapped);
    }
}