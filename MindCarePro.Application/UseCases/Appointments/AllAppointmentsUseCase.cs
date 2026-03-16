using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Appointments;

public class AllAppointmentsUseCase(
    IAppointmentRepository appointmentRepository,
    ICurrentUser currentUser)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    
    public async Task<Result<IEnumerable<Appointment>>> Execute()
    {
        if (_currentUser.UserId is null)
        {
            return Result<IEnumerable<Appointment>>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var appointments = await _appointmentRepository.GetAll(userId);
        return Result<IEnumerable<Appointment>>.Success(appointments);
    }
}
