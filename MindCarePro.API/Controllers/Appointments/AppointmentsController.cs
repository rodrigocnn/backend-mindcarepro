using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Application.UseCases.Appointments;
using MindCarePro.Domain.Shared;
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
        var result = await _createAppointmentUseCase.Execute(request);
        if (result.IsFailure)
        {
            return ResultFailure<AppointmentResponse>(result);
        }

        var appointmentResponse = _mapper.Map<AppointmentResponse>(result.Value!);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All()
    {
        var result = await _allAppointmentsUseCase.Execute();
        if (result.IsFailure)
        {
            return ResultFailure<IEnumerable<AppointmentResponse>>(result);
        }

        var appointmentResponses = _mapper.Map<IEnumerable<AppointmentResponse>>(result.Value!);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponse>>(appointmentResponses));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentRequest request)
    {
        var result = await _updateAppointmentUseCase.Execute(id, request);
        if (result.IsFailure)
        {
            return ResultFailure<AppointmentResponse>(result);
        }

        var appointmentResponse = _mapper.Map<AppointmentResponse>(result.Value!);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteAppointmentUseCase.Execute(id);
        if (result.IsFailure)
        {
            return ResultFailure<AppointmentResponse>(result);
        }

        var appointmentResponse = _mapper.Map<AppointmentResponse>(result.Value!);
        return Ok(new ApiResponse<AppointmentResponse>(appointmentResponse));
    }

    private IActionResult ResultFailure<T>(Result result)
    {
        return StatusCode(ResultStatusCodeHelper.ToStatusCode(result.ErrorType), new ApiResponse<T?>(
            data: default,
            notifications: result.Errors,
            success: false
        ));
    }
}
