using MindCarePro.Application.Dtos.Auth;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Security;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.Interfaces.Users;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Users;

public class LoginUserUseCase(
        IUserRepository userRepository,
        IValidationService validationService,
        IPasswordEncripter passwordEncripter, 
        ITokenService tokenService
    )
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidationService _validationService = validationService;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<LoginResponse>> Execute(string email, string password)
    {
        await _validationService.ValidateAsync(new LoginRequest
        {
            Email = email,
            Password = password
        });

        var user = await _userRepository.GetByEmailAsync(email);
        
        if (user is null)
            return Result<LoginResponse>.Failure(ResultErrorType.Unauthorized, "Usuário ou senha inválidos");

        var valid = _passwordEncripter.Verify(password, user.Password);
        
        
        if (!valid)
            return Result<LoginResponse>.Failure(ResultErrorType.Unauthorized, "Usuário ou senha inválidos");

        var tokenResult= _tokenService.GenerateToken(user.Id, user.Email, user.Name);

        // Tipo do usuário baseado na herança
        var role = user.GetType().Name;
 
        var expiration = tokenResult.Expiration.ToString("yyyy-MM-dd HH:mm:ss");
            
        return Result<LoginResponse>.Success(new LoginResponse(user.Id,  user.Email,user.Name,  role, tokenResult.Token, expiration));
    }
}
