using PanPan.WebApp.Models.Request.Candidato;
using PanPan.WebApp.Models.Response.Candidato;
using PanPan.WebApp.Models.ViewModels.Candidatos;
using Zypher.Http.Extensions;
using Zypher.Json;
using Zypher.Notifications.Interfaces;

namespace PanPan.WebApp.HttpServices;
public class CandidatoHttpService : HttpServiceBase, ICandidatoHttpService
{
    public CandidatoHttpService(HttpClient httpClient, INotificationHandler notification, ILogger<CandidatoHttpService> logger)
        : base(httpClient, notification, logger) { }

    public async Task<CandidatosPaginatedResponse?> ObterPaginado(CandidatosViewModel request)
    {
        var uri = $"api/v1/candidato/paginado{request.ToQueryStringTemplate()}";

        var response = await GetAsync(uri.FormatRouteWithTemplate(request));

        return await ValidarERetornar<CandidatosPaginatedResponse>(response);
    }
    public async Task<CandidatoResponse?> ObterPorId(Guid id)
    {
        var uri = "api/v1/candidato/{id}";

        var response = await GetAsync(uri.FormatRouteWithTemplate(id));

        return await ValidarERetornar<CandidatoResponse>(response);
    }
    public async Task<byte[]> GerarParecer(Guid id, string resp)
    {
        var uri = "api/v1/parecer/candidato/{id}?resp={resp}";

        var response = await GetAsync(uri.FormatRouteWithTemplate(id, resp));

        return await ValidarERetornar<byte[]>(response);
    }

    public async Task Adicionar(AddCandidatoRequest request)
    {
        var uri = "api/v1/candidato";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PostAsync(uri.FormatRouteWithTemplate(), content);

        await ValidarResponse(response);
    }
    public async Task Alterar(Guid id, UpdateCandidatoRequest request)
    {
        var uri = "api/v1/candidato/{id}";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PutAsync(uri.FormatRouteWithTemplate(id), content);

        await ValidarResponse(response);
    }
    public async Task Remover(Guid id)
    {
        var uri = "api/v1/candidato/{id}";

        var response = await DeleteAsync(uri.FormatRouteWithTemplate(id));

        await ValidarResponse(response);
    }

    public async Task<IEnumerable<FormacaoResponse>?> ObterFormacoes(Guid candidatoId)
    {
        var uri = "api/v1/candidato/{candidatoId}/formacao";

        var response = await GetAsync(uri.FormatRouteWithTemplate(candidatoId));

        return await ValidarERetornar<IEnumerable<FormacaoResponse>>(response);
    }
    public async Task Adicionar(Guid id, AddFormacaoRequest request)
    {
        var uri = "api/v1/candidato/{id}/formacao";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PostAsync(uri.FormatRouteWithTemplate(id), content);

        await ValidarResponse(response);
    }
    public async Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var uri = "api/v1/candidato/{id}/formacao/{formacaoId}";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PutAsync(uri.FormatRouteWithTemplate(id, formacaoId), content);

        await ValidarResponse(response);
    }
    public async Task RemoverFormacao(Guid id, Guid formacaoId)
    {
        var uri = "api/v1/candidato/{id}/formacao/{formacaoId}";

        var response = await DeleteAsync(uri.FormatRouteWithTemplate(id, formacaoId));

        await ValidarResponse(response);
    }

    public async Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId)
    {
        var uri = "api/v1/candidato/{candidatoId}/experiencia";

        var response = await GetAsync(uri.FormatRouteWithTemplate(candidatoId));

        return await ValidarERetornar<IEnumerable<ExperienciaResponse>>(response);
    }
    public async Task Adicionar(Guid id, AddExperienciaRequest request)
    {
        var uri = "api/v1/candidato/{id}/experiencia";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PostAsync(uri.FormatRouteWithTemplate(id), content);

        await ValidarResponse(response);
    }
    public async Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var uri = "api/v1/candidato/{id}/experiencia/{experienciaId}";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PutAsync(uri.FormatRouteWithTemplate(id, experienciaId), content);

        await ValidarResponse(response);
    }
    public async Task RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var response = await DeleteAsync(uri.FormatRouteWithTemplate(id, experienciaId));

        await ValidarResponse(response);
    }
}
