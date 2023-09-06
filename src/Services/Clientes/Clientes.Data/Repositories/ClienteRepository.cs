using Api.Core;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ClienteContext _context;

    public ClienteRepository(ClienteContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Cliente>?> ObterTodos() => await _context.Clientes.ToListAsync();
    public async Task<Cliente?> ObterPorId(Guid id) => await _context.Clientes.FindAsync(id);
    public void Adicionar(Cliente cliente) => _context.Clientes.Add(cliente);
    public void Atualizar(Cliente cliente) => _context.Clientes.Update(cliente);
    public void Remover(Cliente cliente) => _context.Clientes.Remove(cliente);
    public void Dispose() => _context.Dispose();
}

