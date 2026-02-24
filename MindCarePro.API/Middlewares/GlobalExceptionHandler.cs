using MindCarePro.API.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using FluentValidation;
using System.Text.Json;
using System.Reflection;

namespace MindCarePro.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Erro detectado pelo Middleware Global: {Message}", exception.Message);
        _logger.LogError(exception, "TIPO EXATO DA EXCEÇÃO: {Type}", exception.GetType().FullName);
        _logger.LogError("STACK TRACE: {Stack}", exception.StackTrace);

        // Chamamos o método privado para obter o status e as notificações
        var (statusCode, notifications) = MapException(exception);

        var response = new ApiResponse<object?>(
            data: null, 
            notifications: notifications, 
            success: false
        );

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true; 
    }

    /// <summary>
    /// Centraliza a inteligência de mapeamento de tipos de erro para Status Codes e Mensagens.
    /// </summary>
    private (int StatusCode, IEnumerable<string> Notifications) MapException(Exception exception)
    {
        return exception switch
        {
            // 1. Erros de Validação (FluentValidation)
            ValidationException v => 
                ((int)HttpStatusCode.BadRequest, v.Errors.Select(e => e.ErrorMessage)),

            // 2. Erros de Entrada e Construção do Objeto (Onde o seu erro estava caindo!)
            BadHttpRequestException or 
                JsonException or 
                ArgumentException or 
                TargetInvocationException or 
                InvalidOperationException => 
                ((int)HttpStatusCode.BadRequest, new[] { "Os dados enviados estão em um formato inválido ou campos obrigatórios não foram preenchidos corretamente." }),

            // 3. Erro de Autorização
            UnauthorizedAccessException => 
                ((int)HttpStatusCode.Unauthorized, new[] { "Acesso não autorizado." }),

            // 4. Erro Genérico (Fallback para erros reais de servidor)
            _ => 
                ((int)HttpStatusCode.InternalServerError, new[] { "Ocorreu um erro interno inesperado no servidor." })
        };
    }
}