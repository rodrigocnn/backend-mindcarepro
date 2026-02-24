using AutoMapper;
using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.Enums.MindCarePro.Application.Enums;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Utils;
using MindCarePro.Domain.Entities.Appointments;

namespace MindCarePro.Application.UseCases.Appointments;

public class CreateAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IValidationService _validationService;
    private readonly IMapper _mapper;

    public CreateAppointmentUseCase(
        IAppointmentRepository appointmentRepository,
        IValidationService validationService,
        IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _validationService = validationService;
        _mapper = mapper;
    }

    public async Task<Appointment> Execute(CreateAppointmentRequest request)
    {
        await _validationService.ValidateAsync(request);
        
        var (backgroundColor, textColor) =
            AppointmentColors.GetColors(AppointmentStatus.Schedule);
        request.Status = AppointmentStatus.Schedule;
        
        var appointment = _mapper.Map<Appointment>(request);
        appointment.UpdateStatus(AppointmentStatus.Schedule, backgroundColor, textColor,"block" );
        
        await _appointmentRepository.Add(appointment);
        return appointment;
    }
}