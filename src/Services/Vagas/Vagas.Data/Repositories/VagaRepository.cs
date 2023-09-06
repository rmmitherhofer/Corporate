using Api.Core;
using Microsoft.EntityFrameworkCore;
using Vagas.Business.Interfaces.Repositories;
using Vagas.Business.Models;
using Vagas.Data;

namespace Vagas.Data.Repositories;

public class VagaRepository : IVagaRepository
{
    private readonly VagasContext _context;

    public VagaRepository(VagasContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Vaga>> ObterTodos() => await _context.Vagas.ToListAsync();
    public async Task<Vaga?> ObterPorId(Guid id) => await _context.Vagas.FindAsync(id);
    public void Adicionar(Vaga vaga) => _context.Vagas.Add(vaga);
    public void Atualizar(Vaga vaga) => _context.Vagas.Update(vaga);
    public void Remover(Vaga vaga) => _context.Vagas.Remove(vaga);
    public void Dispose() => _context.Dispose();
}

