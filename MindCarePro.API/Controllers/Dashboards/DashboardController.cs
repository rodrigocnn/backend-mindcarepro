using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Dashboards;
using MindCarePro.Application.UseCases.Dashboards;
using MindCarePro.Domain.Shared;

namespace MindCarePro.API.Controllers.Dashboards;

[Route("api/dashboard")]
[ApiController]
public class DashboardController(GetDashboardUseCase getDashboardUseCase) : ControllerBase
{
    private readonly GetDashboardUseCase _getDashboardUseCase = getDashboardUseCase;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var result = await _getDashboardUseCase.Execute();
        if (result.IsFailure)
        {
            return ResultFailure<DashboardResponse>(result);
        }

        return Ok(new ApiResponse<DashboardResponse>(result.Value!));
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
