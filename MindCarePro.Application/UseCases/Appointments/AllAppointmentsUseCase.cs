using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Domain.Entities.Appointments;

namespace MindCarePro.Application.UseCases.Appointments;

public class AllAppointmentsUseCase(IAppointmentRepository appointmentRepository)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    
    public async Task<IEnumerable<Appointment>>Execute()
    {
        return await  _appointmentRepository.GetAll();
    }
}

