using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class AllPsychologistsUseCase(IPsychologistRepository psychologistRepository)
{
    private readonly IPsychologistRepository _psychologistRepository =  psychologistRepository;
    
    public async Task<IEnumerable<Psychologist>>Execute()
    {
        return await _psychologistRepository.GetAll();
    }

}