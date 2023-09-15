using Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Http;

public abstract class MainHttpService
{
    protected HttpClient _httpClient;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    protected MainHttpService(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(HttpClient));
        _httpClient = httpClient;
    }

    protected MainHttpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(HttpClient));
        ArgumentNullException.ThrowIfNull(httpContextAccessor, nameof(IHttpContextAccessor));

        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    protected Task<HttpResponseMessage> GetAsync(string uri)
    {
        return _httpClient.GetAsync(uri);
    }

    protected HttpResponseMessage Get(string uri)
    {
        return _httpClient.GetAsync(uri).Result;
    }

    protected Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
    {
        return _httpClient.PostAsync(uri, content);
    }

    protected HttpResponseMessage Post(string uri, HttpContent content)
    {
        return _httpClient.PostAsync(uri, content).Result;
    }

    protected Task<HttpResponseMessage> PutAsync(string uri, HttpContent content)
    {
        return _httpClient.PutAsync(uri, content);
    }

    protected HttpResponseMessage Put(string uri, HttpContent content)
    {
        return _httpClient.PutAsync(uri, content).Result;
    }

    protected Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        return _httpClient.DeleteAsync(uri);
    }

    protected HttpResponseMessage Delete(string uri)
    {
        return _httpClient.DeleteAsync(uri).Result;
    }

    protected StringContent SerializeContent(object data)
    {
        var content = JsonConvert.SerializeObject(data);

        return new(content, Encoding.UTF8, "application/json");
    }

    protected static async Task<T?> DeserializeObjectResponseAsync<T>(HttpResponseMessage httpResponse)
        => JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());

    protected static T? DeserializeObjectResponse<T>(HttpResponseMessage httpResponse)
        => JsonConvert.DeserializeObject<T>(httpResponse.Content.ReadAsStringAsync().Result);

    protected static bool ResponsePossuiErros(HttpResponseMessage response, bool throwException = true)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.BadGateway:
            case HttpStatusCode.BadRequest:
                return true;
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.InternalServerError:
                if(throwException)
                    throw new CustomHttpRequestException(response.StatusCode, $"{response.RequestMessage.Method} - {response.RequestMessage.RequestUri} - {(int)response.StatusCode} - {response.StatusCode}");

                return true;
        }

        try
        {
            response.EnsureSuccessStatusCode();
            return false;
        }
        catch
        {
            return true;
        }
    }
    protected void SetBearerToken(string token) => _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
}