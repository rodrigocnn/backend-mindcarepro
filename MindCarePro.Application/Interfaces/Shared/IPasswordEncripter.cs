namespace MindCarePro.Application.Interfaces.Shared;

public interface IPasswordEncripter
{
    string  Encrypt(string password);
    bool Verify(string password, string passwordHash);
}