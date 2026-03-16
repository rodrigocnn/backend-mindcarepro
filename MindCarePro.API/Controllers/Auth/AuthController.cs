using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Auth;
using MindCarePro.Domain.Shared;
using MindCarePro.Application.UseCases.Users;

namespace MindCarePro.API.Controllers.Auth;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    LoginUserUseCase loginUserUseCase
    
    ) : ControllerBase
{
    private readonly LoginUserUseCase _loginUserUseCase = loginUserUseCase;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _loginUserUseCase.Execute(request.Email, request.Password);
        if (result.IsFailure)
        {
            return ResultFailure<LoginResponse>(result);
        }

        return Ok(new ApiResponse<LoginResponse>(result.Value!));
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
