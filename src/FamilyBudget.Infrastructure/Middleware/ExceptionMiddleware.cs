using FamilyBudget.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FamilyBudget.Infrastructure.Middleware;

internal class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError("[ERROR] {Name} - {ExceptionMessage}\\n{ExceptionStackTrace}\\n{ExceptionInnerException}",
                exception.GetType().Name, exception.Message, exception.StackTrace, exception.InnerException?.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            DomainException => (StatusCodes.Status400BadRequest, new Error(exception.GetType().Name, exception.Message)),
            AuthException => (StatusCodes.Status401Unauthorized, new Error(exception.GetType().Name, exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private sealed record Error(string Code, string Reason);
}