using PanPan.WebApp.Models.Request.Cliente;
using PanPan.WebApp.Models.Response.Cliente;
using PanPan.WebApp.Models.ViewModels.Clientes;
using Zypher.Extensions.Core;
using Zypher.Http.Extensions;
using Zypher.Notifications.Interfaces;

namespace PanPan.WebApp.HttpServices;

public interface IClienteHttpService
{ 
    Task<ClientesPaginatedResponse> ObterPaginado(ClientesViewModel request);
    Task<ClienteResponse> ObterPorId(Guid id);
    Task Adicionar(AddClienteRequest request);
    Task Alterar(Guid id, UpdateClienteRequest request);
    Task Remover(Guid id);
}

public class ClienteHttpService : HttpServiceBase, IClienteHttpService
{
    public ClienteHttpService(HttpClient httpClient, INotificationHandler notification, ILogger<ClienteHttpService> logger) : base(httpClient, notification, logger) { }

    public async Task<ClientesPaginatedResponse> ObterPaginado(ClientesViewModel request)
    {
        var uri = $"api/v1/cliente/paginado?PageNumber={request.PageNumber}&PageSize={request.PageSize}&OrderBy={request.OrderBy}";

        if (!string.IsNullOrEmpty(request.Nome))
            uri += $"&NomeFantasia={request.Nome}";

        if (!string.IsNullOrEmpty(request.Email))
            uri += $"&Email={request.Email}";

        if (!string.IsNullOrEmpty(request.Telefone))
            uri += $"&PhoneNumber={request.Telefone}";

        if (!string.IsNullOrEmpty(request.Celular))
            uri += $"&MobileNumber={request.Celular}";

        if (!string.IsNullOrEmpty(request.Representante))
            uri += $"&NomeRepresentante={request.Representante}";

        if (!string.IsNullOrEmpty(request.Cnpj))
            uri += $"&Cnpj={request.Cnpj.RemoveMaskCpfCnpj()}";

        if (!string.IsNullOrEmpty(request.Cidade))
            uri += $"&City={request.Cidade}";

        if (!string.IsNullOrEmpty(request.Estado))
            uri += $"&State={request.Estado}";

        var response = await GetAsync(uri);

        return await ValidarERetornar<ClientesPaginatedResponse>(response);
    }

    public async Task<ClienteResponse> ObterPorId(Guid id)
    {
        var uri = "api/v1/cliente/{id}";

        return await ObterAsync<ClienteResponse>(uri.FormatRouteWithTemplate(id));
    }
    public async Task Adicionar(AddClienteRequest request)
    {
        var uri = "api/v1/cliente";

        await AdicionarAsync(uri.FormatRouteWithTemplate(), request);
    }
    public async Task Alterar(Guid id, UpdateClienteRequest request)
    {
        var uri = "api/v1/cliente/{id}";

        await AlterarAsync(uri.FormatRouteWithTemplate(id), request);
    }
    public async Task Remover(Guid id)
    {
        var uri = "api/v1/cliente/{id}";

        await RemoverAsync(uri.FormatRouteWithTemplate(id));
    }
}