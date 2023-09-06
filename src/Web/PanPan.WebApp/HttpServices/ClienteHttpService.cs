using Http;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.HttpServices;

public class ClienteHttpService : MainHttpService, IClienteHttpService
{
    public ClienteHttpService(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
    {
        var uri = "api/v1/cliente";

        var response = await GetAsync(uri);

        ResponsePossuiErros(response);

        return await DeserializeObjectResponseAsync<IEnumerable<ClienteViewModel>>(response);
    }

    public async Task Adicionar(AddClienteRequest request)
    {
        var uri = "api/v1/cliente";

        var content = SerializeContent(request);

        var response = await PostAsync(uri, content);

        ResponsePossuiErros(response);
    }

    public async Task Alterar(UpdateClienteRequest request)
    {
        var uri = "api/v1/cliente";

        var content = SerializeContent(request);

        var response = await PutAsync(uri, content);

        ResponsePossuiErros(response);
    }
}