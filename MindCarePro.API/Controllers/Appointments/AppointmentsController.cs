using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.UseCases.Appointments;
using ValidationException = FluentValidation.ValidationException;

namespace MindCarePro.API.Controllers.Appointments;

[Route("api/appointments")]
[ApiController]
public class AppointmentsController(
    CreateAppointmentUseCase createAppointmentUseCase,
    AllAppointmentsUseCase allAppointmentsUseCase,
    UpdateAppointmentUseCase updateAppointmentUseCase,
    DeleteAppointmentUseCase deleteAppointmentUseCase,
    IMapper mapper
) : ControllerBase
{
    private readonly CreateAppointmentUseCase _createAppointmentUseCase = createAppointmentUseCase;
    private readonly AllAppointmentsUseCase _allAppointmentsUseCase = allAppointmentsUseCase;
    private readonly UpdateAppointmentUseCase _updateAppointmentUseCase = updateAppointmentUseCase;
    private readonly DeleteAppointmentUseCase _deleteAppointmentUseCase = deleteAppointmentUseCase;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        
        var appointment = await _createAppointmentUseCase.Execute(request);
        var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var appointments = await _allAppointmentsUseCase.Execute();
        var appointmentResponses = _mapper.Map<IEnumerable<AppointmentResponse>>(appointments);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponse>>(appointmentResponses));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentUseCase request)
    {
        var appointment = await _updateAppointmentUseCase.Execute(id, request);
        var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var appointment = await _deleteAppointmentUseCase.Execute(id);
        var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }
}
