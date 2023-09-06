using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.HttpServices;

public interface IVagaHttpService
{
    Task<IEnumerable<VagaViewModel>> ObterTodos();
    Task Adicionar(AddVagaRequest request);
    Task Alterar(UpdateVagaRequest request);
}
