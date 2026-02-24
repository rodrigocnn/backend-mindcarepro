using MindCarePro.Domain.Entities.Appointments;

namespace MindCarePro.Application.Interfaces.Appointments;

public interface IAppointmentRepository
{
    Task Add(Appointment appointment);
    
    Task<Appointment?> GetById(Guid id);

    Task<IEnumerable<Appointment>> GetAll();

    Task Update(Appointment appointment);

    Task Delete(DateTime id);
}