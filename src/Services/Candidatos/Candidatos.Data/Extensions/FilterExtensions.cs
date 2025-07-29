using Candidatos.Business.Filters;
using Candidatos.Business.Models;

namespace Candidatos.Data.Extensions;

public static class FilterExtensions
{
    public static IQueryable<Candidato> Filter(this IQueryable<Candidato> query, CandidatoFilter filter)
    {
        if(filter.Id.HasValue)
            query = query.Where(c => c.Id == filter.Id.Value);

        if (filter.Codigo.HasValue)
            query = query.Where(c => c.Codigo == filter.Codigo.Value);

        if (!string.IsNullOrEmpty(filter.Nome))
            query = query.Where(c => c.Nome.Trim().ToLower().Contains(filter.Nome.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(c => c.Email.Trim().ToLower().Contains(filter.Email.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Telefone))
            query = query.Where(c => c.Telefone.Trim().ToLower().Contains(filter.Telefone.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Celular))
            query = query.Where(c => c.Celular.Trim().ToLower().Contains(filter.Celular.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.CelularAlternativo))
            query = query.Where(c => c.CelularAlternativo.Trim().ToLower().Contains(filter.CelularAlternativo.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Bairro))
            query = query.Where(c => c.Endereco.Bairro.Trim().ToLower().Contains(filter.Bairro.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Cidade))
            query = query.Where(c => c.Endereco.Cidade.Trim().ToLower().Contains(filter.Cidade.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Estado))
            query = query.Where(c => c.Endereco.Estado.Trim().ToLower().Contains(filter.Estado.Trim().ToLower()));

        if (filter.Sexo.HasValue)
            query = query.Where(c => c.Sexo == filter.Sexo.Value);

        if (filter.Status.HasValue)
            query = query.Where(c => c.Status == filter.Status.Value);

        if (filter.DataInicio.HasValue)
            query = query.Where(c => c.RegistrationDate >=  filter.DataInicio.Value);

        if (filter.DataFim.HasValue)
            query = query.Where(c => c.DateChanged <= filter.DataFim.Value);

        return query;
    }
}
