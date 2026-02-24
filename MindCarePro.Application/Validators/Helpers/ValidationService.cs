using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MindCarePro.Application.Interfaces;


namespace MindCarePro.Application.Helpers;

public class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
    private readonly IServiceProvider _serviceProvide = serviceProvider;
   
    public async Task ValidateAsync<T>(T instance)
    {
        var validator = _serviceProvide.GetRequiredService<IValidator<T>>();

        var result = await validator.ValidateAsync(instance);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}