using AutoMapper;

using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Shared;
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

        var patientMapped = _mapper.Map(request, appointment);
        await _appointmentRepository.Update(patientMapped);

        return Result<Appointment>.Success(patientMapped);
    }
}
