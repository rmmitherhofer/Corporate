using Api.Core;
using Clientes.Business.Filters;
using Clientes.Business.Models;

namespace Clientes.Business.Interfaces.Repositories;

public interface IClienteRepository : IRepository<Cliente> {
    public Task<(IEnumerable<Cliente> clientes, int pageCount, int totalRecords)> ObterPaginado(ClienteFilter filter);
    public Task<Cliente> ObterPorCnpj(string cnpj);
    
}
