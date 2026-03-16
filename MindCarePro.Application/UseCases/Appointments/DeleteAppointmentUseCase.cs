using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Shared;


namespace MindCarePro.Application.UseCases.Appointments;


public class DeleteAppointmentUseCase(
    IAppointmentRepository appointmentRepository,
    ICurrentUser currentUser)
{
    private readonly  IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<Appointment>> Execute(Guid id)
    {
        if (_currentUser.UserId is null)
        {
            return Result<Appointment>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var appointment = await _appointmentRepository.GetById(id, userId);
        if (appointment == null)
        {
            return Result<Appointment>.Failure(ResultErrorType.NotFound, "Agendamento não encontrado");
        }
        
        appointment.Delete(DateTime.UtcNow);
        await _appointmentRepository.Update(appointment);

        return Result<Appointment>.Success(appointment);
    }
}
