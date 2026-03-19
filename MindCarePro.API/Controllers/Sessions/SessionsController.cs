using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Sessions;
using MindCarePro.Application.UseCases.Sessions;
using MindCarePro.Domain.Shared;

namespace MindCarePro.API.Controllers.Sessions;

[Route("api/sessions")]
[ApiController]
public class SessionsController(
    CreateSessionUseCase createSessionUseCase,
    AllSessionsUseCase allSessionsUseCase,
    AllSessionsByPatientUseCase allSessionsByPatientUseCase,
    ShowSessionUseCase showSessionUseCase,
    UpdateSessionUseCase updateSessionUseCase,
    AttendedPatientUseCase attendPatientUseCase,
    IMapper mapper
) : ControllerBase
{
    private readonly CreateSessionUseCase _createSessionUseCase = createSessionUseCase;
    private readonly AllSessionsUseCase _allSessionsUseCase = allSessionsUseCase;
    private readonly AllSessionsByPatientUseCase _allSessionsByPatientUseCase = allSessionsByPatientUseCase;
    private readonly ShowSessionUseCase _showSessionUseCase = showSessionUseCase;
    private readonly UpdateSessionUseCase _updateSessionUseCase = updateSessionUseCase;
    private readonly AttendedPatientUseCase _attendPatientUseCase = attendPatientUseCase;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateSessionRequest request)
    {
        var result = await _createSessionUseCase.Execute(request);
        if (result.IsFailure)
        {
            return ResultFailure<SessionResponse>(result);
        }

        var sessionResponse = _mapper.Map<SessionResponse>(result.Value!);
        return Ok(new ApiResponse<SessionResponse>(sessionResponse));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All()
    {
        var result = await _allSessionsUseCase.Execute();
        if (result.IsFailure)
        {
            return ResultFailure<IEnumerable<SessionResponse>>(result);
        }

        var sessionResponses = _mapper.Map<IEnumerable<SessionResponse>>(result.Value!);
        return Ok(new ApiResponse<IEnumerable<SessionResponse>>(sessionResponses));
    }

    [HttpGet("{patientId:guid}")]
    [Authorize]
    public async Task<IActionResult> AllByPatient(Guid patientId)
    {
        var result = await _allSessionsByPatientUseCase.Execute(patientId);
        if (result.IsFailure)
        {
            return ResultFailure<IEnumerable<SessionResponse>>(result);
        }

        var sessionResponses = _mapper.Map<IEnumerable<SessionResponse>>(result.Value!);
        return Ok(new ApiResponse<IEnumerable<SessionResponse>>(sessionResponses));
    }

    [HttpGet("show/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Show(Guid id)
    {
        var result = await _showSessionUseCase.Execute(id);
        if (result.IsFailure)
        {
            return ResultFailure<SessionResponse>(result);
        }

        var sessionResponse = _mapper.Map<SessionResponse>(result.Value!);
        return Ok(new ApiResponse<SessionResponse>(sessionResponse));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSessionRequest request)
    {
        var result = await _updateSessionUseCase.Execute(id, request);
        if (result.IsFailure)
        {
            return ResultFailure<SessionResponse>(result);
        }

        var sessionResponse = _mapper.Map<SessionResponse>(result.Value!);
        return Ok(new ApiResponse<SessionResponse>(sessionResponse));
    }

    [HttpGet("attend/{patientId:guid}")]
    [Authorize]
    public async Task<IActionResult> AttendedPatient(Guid patientId)
    {
        var result = await _attendPatientUseCase.Execute(patientId);
        if (result.IsFailure)
        {
            return ResultFailure<AttendPatientResponse>(result);
        }

        return Ok(new ApiResponse<AttendPatientResponse>(result.Value!));
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
