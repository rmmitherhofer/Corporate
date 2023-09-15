using Exceptions;
using Http;
using Notifications.Interfaces;
using Notifications.Messages;
using PanPan.WebApp.Models.Response;
using System.Net;

namespace PanPan.WebApp.HttpServices;

public abstract class HttpService : MainHttpService
{
    private readonly INotificationHandler _notification;
    protected HttpService(HttpClient httpClient, INotificationHandler notification) : base(httpClient) => _notification = notification;


    protected async Task<TResponse> ObterAsync<TResponse>(string url)
    {
        var response = await GetAsync(url);

        return ValidarERetornar<TResponse>(response);
    }
    protected async Task AdicionarAsync(string url, object request)
    {
        var content = SerializeContent(request);

        var response = await PostAsync(url, content);

        ValidarResponse(response);
    }
    protected async Task AlterarAsync(string url, object request)
    {
        var content = SerializeContent(request);

        var response = await PutAsync(url, content);

        ValidarResponse(response);
    }
    protected async Task RemoverAsync(string url)
    {
        var response = await DeleteAsync(url);

        ValidarResponse(response);
    }


    protected T ValidarERetornar<T>(HttpResponseMessage response)
    {
        ValidarResponse(response);

        return DeserializeObjectResponse<T>(response);
    }

    protected void ValidarResponse(HttpResponseMessage response)
    {
        if (ResponsePossuiErros(response, false))
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    var errorResponse = DeserializeObjectResponse<ErrorResponse>(response);
                    foreach (var error in errorResponse.Errors)
                    {
                        _notification.Notify(new Notification(error.Level, error.Type, error.Key, error.Value));
                    }
                    break;
                case HttpStatusCode.BadRequest:
                    var validationResponse = DeserializeObjectResponse<ValidationResponse>(response);
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

    protected string GerarQueryString(object request)
    {
        var queryString = string.Empty;
        return queryString;
    }
}
