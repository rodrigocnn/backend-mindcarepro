using MindCarePro.Application.Interfaces.Shared;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MindCarePro.Infrastructure.Security;

public class PasswordEncripter: IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BCryptNet.HashPassword(password);
        return passwordHash;
    }
    
    public bool Verify(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}