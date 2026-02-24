using MindCarePro.API.Common;
using MindCarePro.Application.Dtos.Auth;
using MindCarePro.Application.Dtos.Patients;
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
        try
        {
            var token = await _loginUserUseCase
                .Execute(request.Email, request.Password);

            return Ok(new { token });
        }
        catch (Exception e)
        {
            var response = new ApiResponse<object>(
                data: null,
                notifications: [e.Message],
                success: false
            );

            return BadRequest(response);
        }
     
    }
}
