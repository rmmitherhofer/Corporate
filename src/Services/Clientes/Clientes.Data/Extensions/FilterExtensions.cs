using Clientes.Business.Filters;
using Clientes.Business.Models;

namespace Clientes.Data.Extensions;

public static class FilterExtensions
{
    public static IQueryable<Cliente> Filter(this IQueryable<Cliente> query, ClienteFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Representante))
            query = query.Where(c => c.Representante.Trim().ToLower().Contains(filter.Representante.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Nome))
            query = query.Where(c => c.Nome.Trim().ToLower().Contains(filter.Nome.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Cnpj))
            query = query.Where(c => c.Cnpj.Trim().ToLower().Contains(filter.Cnpj.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(c => c.Email.Trim().ToLower().Contains(filter.Email.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Telefone))
            query = query.Where(c => c.Telefone.Trim().ToLower().Contains(filter.Telefone.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Celular))
            query = query.Where(c => c.Celular.Trim().ToLower().Contains(filter.Celular.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Cidade))
            query = query.Where(c => c.Endereco.Cidade.Trim().ToLower().Contains(filter.Cidade.Trim().ToLower()));

        if (!string.IsNullOrEmpty(filter.Estado))
            query = query.Where(c => c.Endereco.Estado.Trim().ToLower().Contains(filter.Estado.Trim().ToLower()));

        if (filter.DataInicio.HasValue)
            query = query.Where(c => c.DataCadastro >= filter.DataInicio.Value);

        if (filter.DataFim.HasValue)
            query = query.Where(c => c.DataCadastro <= filter.DataFim.Value);

        return query;
    }
}
