using Exceptions;
using Http;
using Notifications.Interfaces;
using Notifications.Messages;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;
using PanPan.WebApp.Models.Response;
using PanPan.WebApp.Models.Response.Candidato;
using System;
using System.Net;

namespace PanPan.WebApp.HttpServices;

public interface ICandidatoHttpService
{
    Task<CandidatosPaginatedResponse> ObterPaginado(CandidatosViewModel request);
    Task<IEnumerable<CandidatoResponse>> ObterTodos();
    Task<CandidatoResponse> ObterPorId(Guid id);
    Task<byte[]> GerarParecer(Guid id);

    Task Incluir(AddCandidatoRequest request);
    Task Alterar(Guid id, UpdateCandidatoRequest request);
    Task Remover(Guid id);

    Task<IEnumerable<FormacaoResponse>> ObterFormacoes(Guid candidatoId);
    Task Incluir(Guid id, AddFormacaoRequest request);
    Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request);
    Task RemoverFormacao(Guid id, Guid formacaoId);

    Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId);
    Task Incluir(Guid id, AddExperienciaRequest request);
    Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request);
    Task RemoverExperiencia(Guid id, Guid experienciaId);

}
public class CandidatoHttpService : MainHttpService, ICandidatoHttpService
{
    private readonly INotificationHandler _notification;
    public CandidatoHttpService(HttpClient httpClient, INotificationHandler notification) : base(httpClient)
    {
        _notification = notification;
    }

    public async Task<CandidatosPaginatedResponse> ObterPaginado(CandidatosViewModel request)
    {
        var uri = $"api/v1/candidato/paginado?PageNumber={request.PageNumber}&PageSize={request.PageSize}&OrderBy={request.OrderBy}";

        if (!string.IsNullOrEmpty(request.Nome))
            uri += $"&Nome={request.Nome}";

        if (!string.IsNullOrEmpty(request.Email))
            uri += $"&Email={request.Email}";

        if (request.Codigo.HasValue)
            uri += $"&Codigo={request.Codigo}";

        if (!string.IsNullOrEmpty(request.Telefone))
            uri += $"&Telefone={request.Telefone}";

        if (!string.IsNullOrEmpty(request.Celular))
            uri += $"&Celular={request.Celular}";

        if (!string.IsNullOrEmpty(request.CelularAlternativo))
            uri += $"&CelularAlternativo={request.CelularAlternativo}";

        if (!string.IsNullOrEmpty(request.Bairro))
            uri += $"&Bairro={request.Bairro}";

        if (!string.IsNullOrEmpty(request.Cidade))
            uri += $"&Cidade={request.Cidade}";

        if (!string.IsNullOrEmpty(request.Estado))
            uri += $"&Estado={request.Estado}";

        if (request.Sexo.HasValue)
            uri += $"&Sexo={request.Sexo}";

        if (request.Status.HasValue)
            uri += $"&Status={request.Status}";

        var response = await GetAsync(uri);

        Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<CandidatosPaginatedResponse>(response);
    }

    public async Task<IEnumerable<CandidatoResponse>> ObterTodos()
    {
        var uri = "api/v1/candidato";

        var response = await GetAsync(uri);

         Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<IEnumerable<CandidatoResponse>>(response);
    }
    public async Task<CandidatoResponse> ObterPorId(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await GetAsync(uri);

        Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<CandidatoResponse>(response);
    }
    public async Task<byte[]> GerarParecer(Guid id)
    {
        var uri = $"api/v1/parecer/candidato/{id}";

        var response = await GetAsync(uri);

        Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<byte[]>(response);
    }

    public async Task Incluir(AddCandidatoRequest request)
    {
        var uri = "api/v1/candidato";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        Notificar(response, "POST", uri);
    }
    public async Task Alterar(Guid id, UpdateCandidatoRequest request)
    {
        var uri = $"api/v1/candidato/{id}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        Notificar(response, "PUT", uri);
    }
    public async Task Remover(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await DeleteAsync(uri);

        Notificar(response, "DELETE", uri);
    }

    public async Task<IEnumerable<FormacaoResponse>> ObterFormacoes(Guid candidatoId)
    {
        var uri = $"api/v1/candidato/{candidatoId}/formacao";

        var response = await GetAsync(uri);

        Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<IEnumerable<FormacaoResponse>>(response);
    }
    public async Task Incluir(Guid id, AddFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        Notificar(response, "POST", uri);
    }
    public async Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        Notificar(response, "PUT", uri);
    }
    public async Task RemoverFormacao(Guid id, Guid formacaoId)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var response = await DeleteAsync(uri);

        Notificar(response, "DELETE", uri);
    }

    public async Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId)
    {
        var uri = $"api/v1/candidato/{candidatoId}/experiencia";

        var response = await GetAsync(uri);

        Notificar(response, "GET", uri);

        return await DeserializeObjectResponseAsync<IEnumerable<ExperienciaResponse>>(response);
    }
    public async Task Incluir(Guid id, AddExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        Notificar(response, "POST", uri);
    }
    public async Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);
        Notificar(response, "PUT", uri);
    }
    public async Task RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var response = await DeleteAsync(uri);

        Notificar(response, "DELETE", uri);
    }


    private void Notificar(HttpResponseMessage response, string method, string uri)
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
                    throw new CustomHttpRequestException($"{method}:{uri} - {response.StatusCode}");
            }
        }
    }


}
