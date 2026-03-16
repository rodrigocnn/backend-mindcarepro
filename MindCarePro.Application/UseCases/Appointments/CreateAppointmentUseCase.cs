using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.Enums.MindCarePro.Application.Enums;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.Utils;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Appointments;

public class CreateAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IValidationService _validationService;
    private readonly ICurrentUser _currentUser;

    public CreateAppointmentUseCase(
        IAppointmentRepository appointmentRepository,
        IValidationService validationService,
        ICurrentUser currentUser)
    {
        _appointmentRepository = appointmentRepository;
        _validationService = validationService;
        _currentUser = currentUser;
    }
 
    public async Task<Result<Appointment>>   Execute(CreateAppointmentRequest request)
    {
        await _validationService.ValidateAsync(request);

        if (_currentUser.UserId is null)
        {
            return Result<Appointment>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }
        
        var userId = _currentUser.UserId.Value;
        var status = AppointmentStatus.Schedule;
        var (backgroundColor, textColor) = AppointmentColors.GetColors(status);

        var appointment = new Appointment(
            title: request.Title,
            start: request.Start!.Value,
            end: request.End!.Value,
            status: status,
            userId: userId,
            patientId: request.PatientId,
            backgroundColor: backgroundColor,
            textColor: textColor,
            display: "block"
        );
        
        await _appointmentRepository.Add(appointment);
        return Result<Appointment>.Success(appointment);
   
    }
}
