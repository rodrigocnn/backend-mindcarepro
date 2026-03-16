using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Shared;
using ValidationException = FluentValidation.ValidationException;

namespace MindCarePro.API.Controllers.Psychologists;

[Route("api/psicologos")]
[ApiController]
public class PsychologistsController(
    CreatePsychologistUseCase createPsychologistUseCase,
    AllPsychologistsUseCase allPsychologistsUseCase,
    UpdatePsychologistUseCase updatePsychologistUseCase,
    DeletePsychologistUseCase deletePsychologistUseCase,
    IMapper mapper
) : ControllerBase
{
    private readonly CreatePsychologistUseCase _createPsychologistUseCase = createPsychologistUseCase;
    private readonly AllPsychologistsUseCase _allPsychologistsUseCase = allPsychologistsUseCase;
    private readonly UpdatePsychologistUseCase _updatePsychologistUseCase = updatePsychologistUseCase;
    private readonly DeletePsychologistUseCase _deletePsychologistUseCase = deletePsychologistUseCase;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<IActionResult> Create(CreatePsychologistRequest request)
    {
        
        var result = await _createPsychologistUseCase.Execute(request);
        if (result.IsFailure)
        {
            return ResultFailure<PsychologistResponse>(result);
        }

        var response = _mapper.Map<PsychologistResponse>(result.Value!);
        return Ok(new ApiResponse<PsychologistResponse>(response));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All()
    {
        var result = await _allPsychologistsUseCase.Execute();
        if (result.IsFailure)
        {
            return ResultFailure<IEnumerable<PsychologistResponse>>(result);
        }

        var response = _mapper.Map<IEnumerable<PsychologistResponse>>(result.Value!);
        return Ok(new ApiResponse<IEnumerable<PsychologistResponse>>(response));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePsychologistRequest request)
    {
        var result = await _updatePsychologistUseCase.Execute(id, request);
        if (result.IsFailure)
        {
            return ResultFailure<PsychologistResponse>(result);
        }

        var response = _mapper.Map<PsychologistResponse>(result.Value!);
        return Ok(new ApiResponse<PsychologistResponse>(response));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deletePsychologistUseCase.Execute(id);
        if (result.IsFailure)
        {
            return ResultFailure<PsychologistResponse>(result);
        }

        var response = _mapper.Map<PsychologistResponse>(result.Value!);
        return Ok(new ApiResponse<PsychologistResponse>(response));
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
