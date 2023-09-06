using Api.Core;
using Candidatos.Business.Interfaces.Repositories;
using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Candidatos.Data.Repositories;

public class CandidatoRepository : ICandidatoRepository
{
    private readonly CandidatoContext _context;

    public CandidatoRepository(CandidatoContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Candidato>> ObterTodos()
    {
        var candidatos = await _context.Candidatos
            .Include(c => c.Formacoes)
            .Include(c => c.Experiencias)
            .ToListAsync();

        return candidatos;
    }

    public async Task<Candidato?> ObterPorId(Guid id) => await _context.Candidatos
            .Include(c => c.Formacoes)
            .Include(c => c.Experiencias)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

    public void Adicionar(Candidato candidato) => _context.Candidatos.Add(candidato);
    public void Atualizar(Candidato candidato) => _context.Candidatos.Update(candidato);
    public void Remover(Candidato candidato) => _context.Candidatos.Remove(candidato);
    public void Adicionar(Formacao formacao) => _context.Formacoes.Add(formacao);
    public void Atualizar(Formacao formacao) => _context.Formacoes.Update(formacao);
    public void Remover(Formacao formacao) => _context.Formacoes.Remove(formacao);
    public void Adicionar(Experiencia experiencia) => _context.Experiencias.Add(experiencia);
    public void Atualizar(Experiencia experiencia) => _context.Experiencias.Update(experiencia);
    public void Remover(Experiencia experiencia) => _context.Experiencias.Remove(experiencia);
    public void Dispose() => _context.Dispose();


}