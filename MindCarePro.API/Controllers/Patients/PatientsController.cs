
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Shared;
using ValidationException = FluentValidation.ValidationException;

namespace MindCarePro.API.Controllers.Patients;

[Route("api/patients")]
[ApiController]
public class PatientsController(
    CreatePatientUseCase createPatientUseCase, 
    AllPatientsUseCase allPatientsUseCase,
    ShowPatientUseCase showPatientUseCase,
    UpdatePatientUseCase updatePatientUseCase,
    DeletePatientUseCase  deletePatientUseCase,
    IMapper mapper): ControllerBase
{
    private readonly CreatePatientUseCase _createPatientUseCase = createPatientUseCase;
    private readonly AllPatientsUseCase _allPatientsUseCase = allPatientsUseCase;
    private readonly ShowPatientUseCase _showPatientUseCase = showPatientUseCase;
    private readonly UpdatePatientUseCase _updatePatientUseCase = updatePatientUseCase;
    private readonly DeletePatientUseCase _deletePatientUseCase = deletePatientUseCase;
    private readonly IMapper _mapper = mapper;
    
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientRequest request)
    {
      
        var result = await _createPatientUseCase.Execute(request);
        if (result.IsFailure)
        {
            return ResultFailure<PatientResponse>(result);
        }
        
        var patientResponse = _mapper.Map<PatientResponse>(result.Value);
        return Ok(new ApiResponse<PatientResponse>(patientResponse));
        
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Show(Guid id)
    {
        var result = await _showPatientUseCase.Execute(id);
        if (result.IsFailure)
        {
            return ResultFailure<PatientResponse>(result);
        }

        var patientResponse = _mapper.Map<PatientResponse>(result.Value!);
        return Ok(new ApiResponse<PatientResponse>(patientResponse));
    }
    
    
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All()
    {
        var result = await _allPatientsUseCase.Execute();
        if (result.IsFailure)
        {
            return ResultFailure<IEnumerable<PatientResponse>>(result);
        }

        var patientResponses = _mapper.Map<IEnumerable<PatientResponse>>(result.Value!);
        return Ok(new ApiResponse<IEnumerable<PatientResponse>>(patientResponses));
    }
    
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update( Guid id, [FromBody] CreatePatientRequest request)
    {
        var result = await _updatePatientUseCase.Execute(id, request);
        if (result.IsFailure)
        {
            return ResultFailure<PatientResponse>(result);
        }

        var patientResponse = _mapper.Map<PatientResponse>(result.Value!);
        return Ok(new ApiResponse<PatientResponse>(patientResponse));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
       var result = await _deletePatientUseCase.Execute(id);
       if (result.IsFailure)
       {
           return ResultFailure<PatientResponse>(result);
       }

       var patientResponse = _mapper.Map<PatientResponse>(result.Value!);
       return Ok(new ApiResponse<PatientResponse>(patientResponse));
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
