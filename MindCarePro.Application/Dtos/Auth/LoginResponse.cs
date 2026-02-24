namespace MindCarePro.Application.Dtos.Auth;

public class LoginResponse(Guid id, string email, string role, string token, string expiration)
{
    public Guid Id { get; } = id;
    public string Email { get; } = email;
    public string Role { get; } = role;
    public string Token { get; } = token;
    public string Expiration { get; } = expiration;
}