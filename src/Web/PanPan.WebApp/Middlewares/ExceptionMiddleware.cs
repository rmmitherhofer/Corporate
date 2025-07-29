using System.Net;
using Zypher.Domain.Exceptions;
using Zypher.Extensions.Core;
using Zypher.Http.Exceptions;
using Zypher.Logs.Extensions;

namespace PanPan.WebApp.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                RedirectToErrorPages(context, new EntryPointNotFoundException("URL não localizada"));

        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _logger.LogWarn(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _logger.LogWarn(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch (CustomHttpRequestException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            _logger.LogWarn(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch (HttpRequestException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            _logger.LogFail(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch( ArgumentNullException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogCrit(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogCrit(ex.Message);
            RedirectToErrorPages(context, ex);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogCrit(ex.Message);
            RedirectToErrorPages(context, ex);
        }
    }

    private void RedirectToErrorPages(HttpContext context, Exception ex)
    {
        AddHeaderResponse(context);

        if (context.Request.IsAjaxRequest())
        {
            context.Response.WriteAsync(ex.InnerException?.Message ?? ex.Message);
            return;
        }

        context.Response.Redirect($"/error/{context.Response.StatusCode}");
    }

    private void AddHeaderResponse(HttpContext context)
    {
        if (context.Response.Headers.ContainsKey(HttpRequestExtensions.CORRELATION_ID))
            context.Response.Headers.Remove(HttpRequestExtensions.CORRELATION_ID);

        context.Response.Headers.TryAdd(HttpRequestExtensions.CORRELATION_ID, context?.Request?.GetCorrelationId() ?? context?.Request?.GetRequestId());
        context.Response.Cookies.Append(HttpRequestExtensions.CORRELATION_ID, context?.Request?.GetCorrelationId() ?? context?.Request?.GetRequestId());
    }
}
