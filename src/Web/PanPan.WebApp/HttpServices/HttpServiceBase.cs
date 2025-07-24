using PanPan.WebApp.Models.Response;
using System.Net;
using Zypher.Http;
using Zypher.Http.Exceptions;
using Zypher.Http.Extensions;
using Zypher.Json;
using Zypher.Notifications.Interfaces;
using Zypher.Notifications.Messages;

namespace PanPan.WebApp.HttpServices;

public abstract class HttpServiceBase : HttpService
{
    protected HttpServiceBase(HttpClient httpClient, INotificationHandler notification, ILogger logger) : base(httpClient, notification, logger) { }

    protected async Task<TResponse?> ObterAsync<TResponse>((string template, string url) uri)
    {
        var response = await GetAsync(uri);

        return await ValidarERetornar<TResponse>(response);
    }
    protected async Task AdicionarAsync((string template, string url) uri, object request)
    {
        var content = JsonExtensions.SerializeContent(request);

        var response = await PostAsync(uri, content);

        await ValidarResponse(response);
    }
    protected async Task AlterarAsync((string template, string url) uri, object request)
    {
        var content = JsonExtensions.SerializeContent(request);

        var response = await PutAsync(uri, content);

        await ValidarResponse(response);
    }
    protected async Task RemoverAsync((string template, string url) uri)
    {
        var response = await DeleteAsync(uri);

        await ValidarResponse(response);
    }

    protected async Task<T?> ValidarERetornar<T>(HttpResponseMessage response)
    {
        await ValidarResponse(response);

        return await response.ReadAsAsync<T>();
    }

    protected async Task ValidarResponse(HttpResponseMessage response)
    {
        if (response.HasErrors(false))
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    var errorResponse = await response.ReadAsAsync<ErrorResponse>();
                    foreach (var error in errorResponse.Errors)
                    {
                        _notification.Notify(new Notification(error.Level, error.Type, error.Key, error.Value));
                    }
                    break;
                case HttpStatusCode.BadRequest:
                    var validationResponse = await response.ReadAsAsync<ValidationResponse>();
                    foreach (var validation in validationResponse.Validations)
                    {
                        _notification.Notify(new Notification(validation.Level, validation.Type, validation.Key, validation.Value));
                    }
                    break;
                default:
                    throw new CustomHttpRequestException($"{response.RequestMessage.Method} - {response.RequestMessage.RequestUri} - {(int)response.StatusCode} - {response.StatusCode}");
            }
        }
    }
}
