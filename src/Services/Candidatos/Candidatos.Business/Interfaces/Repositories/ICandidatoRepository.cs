using Candidatos.Business.Filters;
using Candidatos.Business.Models;
using Zypher.Persistence.Abstractions.Data;

namespace Candidatos.Business.Interfaces.Repositories;

public interface ICandidatoRepository : IRepository<Candidato>
{
    public Task<(IEnumerable<Candidato> candidatos, int pageCount, int totalRecords)> ObterPaginado(CandidatoFilter filter);

    Task<IEnumerable<Formacao>?> ObterFormacoesPorCandidatoId(Guid candidatoId);
    Task<Formacao?> ObterFormacaoPorId(Guid id);
    Task<IEnumerable<Experiencia>?> ObterExperienciasPorCandidatoId(Guid candidatoId);
    Task<Experiencia?> ObterExperienciaPorId(Guid id);

    public void Adicionar(Formacao formacao);
    public void Atualizar(Formacao formacao);
    public void Remover(Formacao formacao);
    public void Adicionar(Experiencia experiencia);
    public void Atualizar(Experiencia experiencia);
    public void Remover(Experiencia experiencia);
}