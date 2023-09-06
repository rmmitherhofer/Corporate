using Api.Core;
using Candidatos.Business.Models;

namespace Candidatos.Business.Interfaces.Repositories;

public interface ICandidatoRepository : IRepository<Candidato>
{
    public void Adicionar(Formacao formacao);
    public void Atualizar(Formacao formacao);
    public void Remover(Formacao formacao);
    public void Adicionar(Experiencia experiencia);
    public void Atualizar(Experiencia experiencia);
    public void Remover(Experiencia experiencia);
}