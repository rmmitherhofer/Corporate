using Api.Service.Models;
using Api.Service.Models.Responses;
using Logs.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Api.Service.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private Stopwatch _diagnostico;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _diagnostico = new();
        _diagnostico.Start();

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleRequestException(context, ex, HttpStatusCode.InternalServerError);
        }
        finally { _diagnostico.Stop(); }
    }

    private void HandleRequestException(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {

        switch (statusCode)
        {
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.BadRequest:
                _logger.LogWarn($"Descricao: {exception.Message} - detalhe: {exception.StackTrace}");
                break;
            case HttpStatusCode.BadGateway:
                _logger.LogFail($"Descricao: {exception.Message} - detalhe: {exception.StackTrace}");
                break;
            case HttpStatusCode.InternalServerError:
                _logger.LogCrit($"Descricao: {exception.Message} - detalhe: {exception.StackTrace}");
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        ApiError error = new(statusCode, exception.Message);
        context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiErrorResponse { Error = error }));
    }
}
