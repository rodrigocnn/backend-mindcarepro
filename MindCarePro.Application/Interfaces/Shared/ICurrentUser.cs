namespace MindCarePro.Application.Interfaces.Shared;

public interface ICurrentUser
{
    Guid? UserId { get; }
    string? Email { get; }
    string? Name { get; }
}