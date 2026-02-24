using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.UseCases.Psychologists;
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

        try
        {
            var psychologist = await _createPsychologistUseCase.Execute(request);
            var response = _mapper.Map<PsychologistResponse>(psychologist);
            return Ok(new ApiResponse<PsychologistResponse>(response));
        }
        catch (ValidationException e)
        {
            var errors = e.Errors.Select(x => x.ErrorMessage);

            var response = new ApiResponse<object>(
                data: null,
                notifications: errors,
                success: false
            );

            return BadRequest(response);
        }
       
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var psychologists = await _allPsychologistsUseCase.Execute();
        var response = _mapper.Map<IEnumerable<PsychologistResponse>>(psychologists);
        return Ok(new ApiResponse<IEnumerable<PsychologistResponse>>(response));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePsychologistRequest request)
    {
        var psychologist = await _updatePsychologistUseCase.Execute(id, request);
        var response = _mapper.Map<PsychologistResponse>(psychologist);
        return Ok(new ApiResponse<PsychologistResponse>(response));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var psychologist = await _deletePsychologistUseCase.Execute(id);
        var response = _mapper.Map<PsychologistResponse>(psychologist);
        return Ok(new ApiResponse<PsychologistResponse>(response));
    }
}
