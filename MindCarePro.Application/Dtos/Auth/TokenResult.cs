namespace MindCarePro.Application.Dtos.Auth;

public record TokenResult(string Token, DateTime Expiration);