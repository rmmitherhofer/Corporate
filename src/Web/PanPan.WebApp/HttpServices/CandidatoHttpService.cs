using Notifications.Interfaces;
using PanPan.WebApp.Models.Request.Candidato;
using PanPan.WebApp.Models.Response.Candidato;
using PanPan.WebApp.Models.ViewModels.Candidatos;

namespace PanPan.WebApp.HttpServices;

public interface ICandidatoHttpService
{
    Task<CandidatosPaginatedResponse> ObterPaginado(CandidatosViewModel request);
    Task<CandidatoResponse> ObterPorId(Guid id);
    Task<byte[]> GerarParecer(Guid id);

    Task Adicionar(AddCandidatoRequest request);
    Task Alterar(Guid id, UpdateCandidatoRequest request);
    Task Remover(Guid id);

    Task<IEnumerable<FormacaoResponse>> ObterFormacoes(Guid candidatoId);
    Task Adicionar(Guid id, AddFormacaoRequest request);
    Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request);
    Task RemoverFormacao(Guid id, Guid formacaoId);

    Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId);
    Task Adicionar(Guid id, AddExperienciaRequest request);
    Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request);
    Task RemoverExperiencia(Guid id, Guid experienciaId);

}
public class CandidatoHttpService : HttpService, ICandidatoHttpService
{
    public CandidatoHttpService(HttpClient httpClient, INotificationHandler notification) : base(httpClient, notification) { }

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

        return ValidarERetornar<CandidatosPaginatedResponse>(response);
    }
    public async Task<CandidatoResponse> ObterPorId(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await GetAsync(uri);

        return ValidarERetornar<CandidatoResponse>(response);
    }
    public async Task<byte[]> GerarParecer(Guid id)
    {
        var uri = $"api/v1/parecer/candidato/{id}";

        var response = await GetAsync(uri);

        return ValidarERetornar<byte[]>(response);
    }

    public async Task Adicionar(AddCandidatoRequest request)
    {
        var uri = "api/v1/candidato";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task Alterar(Guid id, UpdateCandidatoRequest request)
    {
        var uri = $"api/v1/candidato/{id}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task Remover(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await DeleteAsync(uri);

        ValidarResponse(response);
    }

    public async Task<IEnumerable<FormacaoResponse>> ObterFormacoes(Guid candidatoId)
    {
        var uri = $"api/v1/candidato/{candidatoId}/formacao";

        var response = await GetAsync(uri);

        return ValidarERetornar<IEnumerable<FormacaoResponse>>(response);
    }
    public async Task Adicionar(Guid id, AddFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task RemoverFormacao(Guid id, Guid formacaoId)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var response = await DeleteAsync(uri);

        ValidarResponse(response);
    }

    public async Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId)
    {
        var uri = $"api/v1/candidato/{candidatoId}/experiencia";

        var response = await GetAsync(uri);

        return ValidarERetornar<IEnumerable<ExperienciaResponse>>(response);
    }
    public async Task Adicionar(Guid id, AddExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ValidarResponse(response);
    }
    public async Task RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var response = await DeleteAsync(uri);

        ValidarResponse(response);
    }    
}
