using MindCarePro.Application.Dtos.Auth;

namespace MindCarePro.Application.Interfaces.Security;

public interface ITokenService
{
    TokenResult GenerateToken(Guid userId, string email, string name);
}
