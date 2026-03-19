using AutoMapper;

using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.Enums;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.Utils;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Appointments;

public class UpdateAppointmentUseCase(
    IAppointmentRepository appointmentRepository,
    IValidationService validationService,
    ICurrentUser currentUser,
    IMapper mapper)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Appointment>> Execute(Guid id, UpdateAppointmentRequest request)
    {
        await _validationService.ValidateAsync(request);
        var userId = _currentUser.UserId;

        if (userId is null)
        {
            return Result<Appointment>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }
        
        var appointment = await GetAppointment(id, userId.Value);

        if (appointment == null)
        {
            return Result<Appointment>.Failure(ResultErrorType.NotFound, "Agendamento não encontrado");
        }
        
        if (await CheckOverlap(request, appointment, userId.Value))
        {
            return Result<Appointment>.Failure(ResultErrorType.Conflict,
                "Já existe agendamento para esse horario");
        }
        
        var patientMapped = MapAppointment(request, appointment);
        await _appointmentRepository.Update(patientMapped);
        return Result<Appointment>.Success(patientMapped);
        
    }

    private async Task<bool> CheckOverlap(UpdateAppointmentRequest request, Appointment appointment, Guid userId)
    {
        var effectiveStart = request.Start ?? appointment.Start;
        var effectiveEnd = request.End ?? appointment.End;
        var hasOverlap = await _appointmentRepository.HasOverlap(userId, effectiveStart, effectiveEnd, appointment.Id);
        return hasOverlap;
    }

    private Task<Appointment?> GetAppointment(Guid id, Guid userId)
    {
        return _appointmentRepository.GetById(id, userId);
    }

    private Appointment MapAppointment(UpdateAppointmentRequest request, Appointment appointment)
    {
        _mapper.Map(request, appointment);
        var (backgroundColor, textColor) = AppointmentColors.GetColors(request.Status);
        appointment.UpdateStatus(request.Status, backgroundColor, textColor);
        return appointment;
    }

 
    
    
}
