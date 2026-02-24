namespace MindCarePro.Application.Interfaces;

public interface IValidationService
{
    Task ValidateAsync<T>(T instance);
}