using Api.Core;
using Clientes.Business.Filters;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Business.Models;
using Clientes.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Api.Data.Extensions;

namespace Clientes.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ClienteContext _context;

    public ClienteRepository(ClienteContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<(IEnumerable<Cliente> clientes, int pageCount, int totalRecords)> ObterPaginado(ClienteFilter filter)
    {
        var query = _context.Clientes.Filter(filter);

        var totalRecords = await query.CountAsync();

        int pageCount = filter.GetPageCount(totalRecords);

        var clientes = await query
            .OrderBy(filter.OrderBy)
            .Page(filter.PageNumber, filter.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return (clientes, pageCount, totalRecords);
    }
    public async Task<IEnumerable<Cliente>?> ObterTodos() => await _context.Clientes.ToListAsync();
    public async Task<Cliente> ObterPorCnpj(string cnpj) => await _context.Clientes.Where(c => c.Cnpj == cnpj).FirstOrDefaultAsync();
    public async Task<Cliente?> ObterPorId(Guid id) => await _context.Clientes.FindAsync(id);
    public void Adicionar(Cliente cliente) => _context.Clientes.Add(cliente);
    public void Atualizar(Cliente cliente) => _context.Clientes.Update(cliente);
    public void Remover(Cliente cliente) => _context.Clientes.Remove(cliente);
    public void Dispose() => _context.Dispose();


}

