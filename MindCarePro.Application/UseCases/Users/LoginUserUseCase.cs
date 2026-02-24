using MindCarePro.Application.Dtos.Auth;
using MindCarePro.Application.Interfaces.Security;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.Interfaces.Users;

namespace MindCarePro.Application.UseCases.Users;

public class LoginUserUseCase(
        IUserRepository userRepository,
        IPasswordEncripter passwordEncripter, 
        ITokenService tokenService
    )
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<LoginResponse> Execute(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        
 

        if (user is null)
            throw new Exception("Usuário ou senha inválidos");

        var valid = _passwordEncripter.Verify(password, user.Password);
        
        
        if (!valid)
            throw new Exception("Usuário ou senha inválidos");

        var tokenResult= _tokenService.GenerateToken(user.Id, user.Email);

        // Tipo do usuário baseado na herança
        var role = user.GetType().Name;
        var expiration = tokenResult.Expiration.ToString("yyyy-MM-dd HH:mm:ss");
            
        return new LoginResponse( user.Id, user.Email, role, tokenResult.Token, expiration);
    }
}
