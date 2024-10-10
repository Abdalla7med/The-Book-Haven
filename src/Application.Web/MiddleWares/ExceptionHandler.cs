using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
    {
        // Log the exception
        _logger.LogError(ex, ex.Message);

        // Prepare the response
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = new
        {
            statusCode = (int)statusCode,
            message = ex.Message // Optionally, include more details
        };

        return context.Response.WriteAsJsonAsync(result);
    }
}
