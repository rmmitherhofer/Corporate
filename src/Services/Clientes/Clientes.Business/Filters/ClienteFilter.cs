using Api.Core.Data.Filters;

namespace Clientes.Business.Filters;

public class ClienteFilter : Filter
{
    public string? Cnpj { get; set; }
    public string? Nome { get; set; }
    public string? Representante { get; set; }

    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }

    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
