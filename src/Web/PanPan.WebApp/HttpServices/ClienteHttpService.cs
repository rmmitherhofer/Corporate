using Extensions;
using Notifications.Interfaces;
using PanPan.WebApp.Models.Request.Cliente;
using PanPan.WebApp.Models.Response.Candidato;
using PanPan.WebApp.Models.Response.Cliente;
using PanPan.WebApp.Models.ViewModels.Clientes;

namespace PanPan.WebApp.HttpServices;

public interface IClienteHttpService
{ 
    Task<ClientesPaginatedResponse> ObterPaginado(ClientesViewModel request);
    Task<ClienteResponse> ObterPorId(Guid id);
    Task Adicionar(AddClienteRequest request);
    Task Alterar(Guid id, UpdateClienteRequest request);
    Task Remover(Guid id);
}

public class ClienteHttpService : HttpService, IClienteHttpService
{
    public ClienteHttpService(HttpClient httpClient, INotificationHandler notification) : base(httpClient, notification) { }

    public async Task<ClientesPaginatedResponse> ObterPaginado(ClientesViewModel request)
    {
        var uri = $"api/v1/cliente/paginado?PageNumber={request.PageNumber}&PageSize={request.PageSize}&OrderBy={request.OrderBy}";

        if (!string.IsNullOrEmpty(request.Nome))
            uri += $"&NomeFantasia={request.Nome}";

        if (!string.IsNullOrEmpty(request.Email))
            uri += $"&Email={request.Email}";

        if (!string.IsNullOrEmpty(request.Telefone))
            uri += $"&Telefone={request.Telefone}";

        if (!string.IsNullOrEmpty(request.Celular))
            uri += $"&Celular={request.Celular}";

        if (!string.IsNullOrEmpty(request.Representante))
            uri += $"&NomeRepresentante={request.Representante}";

        if (!string.IsNullOrEmpty(request.Cnpj))
            uri += $"&Cnpj={request.Cnpj.RemoverMascaraCpfCnpj()}";

        if (!string.IsNullOrEmpty(request.Cidade))
            uri += $"&Cidade={request.Cidade}";

        if (!string.IsNullOrEmpty(request.Estado))
            uri += $"&Estado={request.Estado}";

        var response = await GetAsync(uri);

        return ValidarERetornar<ClientesPaginatedResponse>(response);
    }

    public async Task<ClienteResponse> ObterPorId(Guid id)
    {
        var uri = $"api/v1/cliente/{id}";

        return await ObterAsync<ClienteResponse>(uri);
    }
    public async Task Adicionar(AddClienteRequest request)
    {
        var uri = "api/v1/cliente";

        await AdicionarAsync(uri, request);
    }
    public async Task Alterar(Guid id, UpdateClienteRequest request)
    {
        var uri = $"api/v1/cliente/{id}";

        await AlterarAsync(uri, request);
    }
    public async Task Remover(Guid id)
    {
        var uri = $"api/v1/cliente/{id}";

        await RemoverAsync(uri);
    }
}