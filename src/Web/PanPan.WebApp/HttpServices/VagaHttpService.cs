using Http;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.HttpServices;

public interface IVagaHttpService
{
    Task<IEnumerable<VagaViewModel>> ObterTodos();
    Task Adicionar(AddVagaRequest request);
    Task Alterar(UpdateVagaRequest request);
}
public class VagaHttpService : MainHttpService, IVagaHttpService
{
    public VagaHttpService(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<VagaViewModel>> ObterTodos()
    {
        var uri = "api/v1/vaga";

        var response = await GetAsync(uri);

        ResponsePossuiErros(response);

        return await DeserializeObjectResponseAsync<IEnumerable<VagaViewModel>>(response);
    }

    public async Task Adicionar(AddVagaRequest request)
    {
        var uri = "api/v1/vaga";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ResponsePossuiErros(response);
    }

    public async Task Alterar(UpdateVagaRequest request)
    {
        var uri = "api/v1/vaga";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ResponsePossuiErros(response);
    }
}