using AutoMapper;

using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.Interfaces.Appointments;

namespace MindCarePro.Application.UseCases.Appointments;

public class UpdateAppointmentUseCase(IAppointmentRepository appointmentRepository, IMapper mapper)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<AppointmentResponse> Execute(Guid id, UpdateAppointmentUseCase request)
    {

        var appointment = await _appointmentRepository.GetById(id);

        if (appointment == null)
        {
            throw new ArgumentNullException("Patient not found");
        }

        var patientMapped = _mapper.Map(request, appointment);
        await _appointmentRepository.Update(patientMapped);

        return _mapper.Map<AppointmentResponse>(patientMapped);
    }
}