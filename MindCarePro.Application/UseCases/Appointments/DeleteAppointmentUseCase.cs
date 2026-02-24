using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Domain.Entities.Appointments;


namespace MindCarePro.Application.UseCases.Appointments;


public class DeleteAppointmentUseCase(IAppointmentRepository appointmentRepository)
{
    private readonly  IAppointmentRepository _appointmentRepository = appointmentRepository;

    public async Task<Appointment> Execute(Guid id)
    {

        var appointment = await _appointmentRepository.GetById(id);
        if (appointment == null)
        {
            throw new Exception($"Appointment with id {id} not found."); // ou NotFoundException
        }
        
        await _appointmentRepository.Delete(DateTime.UtcNow);
        await _appointmentRepository.Update(appointment);

        return appointment;
    }
}