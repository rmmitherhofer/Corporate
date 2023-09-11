using Exceptions;
using Logs.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notifications.Messages;
using Responses;
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
        catch (DomainException ex)
        {
            var notifications = new List<Notification> { new(ex.GetType().Name, ex.GetType().Name, ex.Message, ex?.StackTrace!) };
            HandleRequestException(context, new ValidationResponse(notifications), HttpStatusCode.BadRequest);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleRequestException(context, ex, HttpStatusCode.BadGateway);
        }
        catch (UnauthorizedAccessException ex)
        {
            HandleRequestException(context, ex, HttpStatusCode.Unauthorized);
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

        var notifications = new List<Notification> { new(exception.GetType().Name, exception.GetType().Name, exception.Message, exception?.StackTrace!) };

        ErrorResponse error = new(statusCode, notifications);

        context.Response.WriteAsync(JsonConvert.SerializeObject(error));
    }

    private void HandleRequestException(HttpContext context, ValidationResponse response, HttpStatusCode statusCode)
    {
        foreach (var validation in response.Validations)
            _logger.LogWarn($"Descrição: {validation.Key} - detalhe : {validation.Value}");

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
