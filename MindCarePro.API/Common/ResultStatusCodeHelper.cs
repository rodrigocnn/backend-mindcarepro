using Microsoft.AspNetCore.Http;
using MindCarePro.Domain.Shared;

namespace MindCarePro.API.Common;

public static class ResultStatusCodeHelper
{
    public static int ToStatusCode(ResultErrorType errorType)
    {
        return errorType switch
        {
            ResultErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ResultErrorType.NotFound => StatusCodes.Status404NotFound,
            ResultErrorType.Conflict => StatusCodes.Status409Conflict,
            ResultErrorType.Validation => StatusCodes.Status400BadRequest,
            ResultErrorType.Unspecified => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
