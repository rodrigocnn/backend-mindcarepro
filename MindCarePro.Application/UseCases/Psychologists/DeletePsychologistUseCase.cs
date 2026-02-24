using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.UseCases.Psychologists;

public class DeletePsychologistUseCase(IPsychologistRepository  psychologistRepository)
{
    private readonly IPsychologistRepository _psychologistRepository= psychologistRepository;

    public async Task<Psychologist> Execute(Guid id)
    {

        var psychologist = await _psychologistRepository.GetById(id);
        if ( psychologist== null)
        {
            throw new Exception($"Psychologist with id {id} not found."); // ou NotFoundException
        }
        
        psychologist.DeletedAt = DateTime.UtcNow;
        await _psychologistRepository.Update( psychologist);

        return psychologist;
    }
}