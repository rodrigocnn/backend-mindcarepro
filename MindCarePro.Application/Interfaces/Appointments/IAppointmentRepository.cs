using MindCarePro.Domain.Entities.Appointments;

namespace MindCarePro.Application.Interfaces.Appointments;

public interface IAppointmentRepository
{
    Task Add(Appointment appointment);
    
    Task<Appointment?> GetById(Guid id);
    Task<Appointment?> GetById(Guid id, Guid userId);

    Task<IEnumerable<Appointment>> GetAll();
    Task<IEnumerable<Appointment>> GetAll(Guid userId);

    Task Update(Appointment appointment);

    Task Delete(DateTime id);
}
