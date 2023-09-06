using Http;
using PanPan.WebApp.Models.Request;
using PanPan.WebApp.Models.Response.Candidato;

namespace PanPan.WebApp.HttpServices;

public class CandidatoHttpService : MainHttpService, ICandidatoHttpService
{
    public CandidatoHttpService(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<CandidatoResponse>> ObterTodos()
    {
        var uri = "api/v1/candidato";

        var response = await GetAsync(uri);

        ResponsePossuiErros(response);

        return await DeserializeObjectResponseAsync<IEnumerable<CandidatoResponse>>(response);
    }
    public async Task<CandidatoResponse> ObterPorId(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await GetAsync(uri);

        ResponsePossuiErros(response);

        return await DeserializeObjectResponseAsync<CandidatoResponse>(response);
    }
    public async Task<byte[]> GerarParecer(Guid id)
    {
        var uri = $"api/v1/parecer/candidato/{id}";

        var response = await GetAsync(uri);

        ResponsePossuiErros(response);

        return await DeserializeObjectResponseAsync<byte[]>(response);
    }

    public async Task Incluir(AddCandidatoRequest request)
    {
        var uri = "api/v1/candidato";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task Alterar(Guid id, UpdateCandidatoRequest request)
    {
        var uri = $"api/v1/candidato/{id}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task Remover(Guid id)
    {
        var uri = $"api/v1/candidato/{id}";

        var response = await DeleteAsync(uri);

        ResponsePossuiErros(response);
    }

    public async Task Incluir(Guid id, AddFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task RemoverFormacao(Guid id, Guid formacaoId)
    {
        var uri = $"api/v1/candidato/{id}/formacao/{formacaoId}";

        var response = await DeleteAsync(uri);

        ResponsePossuiErros(response);
    }

    public async Task Incluir(Guid id, AddExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ResponsePossuiErros(response);
    }
    public async Task RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var uri = $"api/v1/candidato/{id}/experiencia/{experienciaId}";

        var response = await DeleteAsync(uri);

        ResponsePossuiErros(response);
    }


}
