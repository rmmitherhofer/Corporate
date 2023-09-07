using Http;
using Microsoft.IdentityModel.Tokens;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;
using PanPan.WebApp.Models.Response.Candidato;

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

    Task Incluir(Guid id, AddFormacaoRequest request);
    Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request);
    Task RemoverFormacao(Guid id, Guid formacaoId);

    Task Incluir(Guid id, AddExperienciaRequest request);
    Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request);
    Task RemoverExperiencia(Guid id, Guid experienciaId);

}
public class CandidatoHttpService : MainHttpService, ICandidatoHttpService
{
    public CandidatoHttpService(HttpClient httpClient) : base(httpClient) { }

    public async Task<CandidatosPaginatedResponse> ObterPaginado(CandidatosViewModel request)
    {
        var uri = $"api/v1/candidato/paginado?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

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

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"GET:{uri} - {response.StatusCode}");

        return await DeserializeObjectResponseAsync<CandidatosPaginatedResponse>(response);
    }

    public async Task<IEnumerable<CandidatoResponse>> ObterTodos()
    {
        var uri = "api/v1/candidato";

        var response = await GetAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"GET:{uri} - {response.StatusCode}");


        return await DeserializeObjectResponseAsync<IEnumerable<CandidatoResponse>>(response);
    }
    public async Task<CandidatoResponse> ObterPorId(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await GetAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"GET:{uri} - {response.StatusCode}");

        return await DeserializeObjectResponseAsync<CandidatoResponse>(response);
    }
    public async Task<byte[]> GerarParecer(Guid id)
    {
        var uri = $"api/v1/parecer/candidato/{id}";

        var response = await GetAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"GET:{uri} - {response.StatusCode}");

        return await DeserializeObjectResponseAsync<byte[]>(response);
    }

    public async Task Incluir(AddCandidatoRequest request)
    {
        var uri = "api/v1/candidato";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"POST:{uri} - {response.StatusCode}");
    }
    public async Task Alterar(Guid id, UpdateCandidatoRequest request)
    {
        var uri = $"api/v1/candidato/{id}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"PUT:{uri} - {response.StatusCode}");
    }
    public async Task Remover(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await DeleteAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"DELETE:{uri} - {response.StatusCode}");
    }

    public async Task Incluir(Guid id, AddFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"POST:{uri} - {response.StatusCode}");
    }
    public async Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"PUT:{uri} - {response.StatusCode}");
    }
    public async Task RemoverFormacao(Guid id, Guid formacaoId)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var response = await DeleteAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"DELETE:{uri} - {response.StatusCode}");
    }

    public async Task Incluir(Guid id, AddExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"POST:{uri} - {response.StatusCode}");
    }
    public async Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"PUT:{uri} - {response.StatusCode}");
    }
    public async Task RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var response = await DeleteAsync(uri);

        if (ResponsePossuiErros(response))
            throw new HttpRequestException($"DELETE:{uri} - {response.StatusCode}");
    }


}
