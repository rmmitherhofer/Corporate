using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.HttpServices;

public interface IClienteHttpService
{
    Task<IEnumerable<ClienteViewModel>> ObterTodos();
    Task Adicionar(AddClienteRequest request);
    Task Alterar(UpdateClienteRequest request);
}
