using Api.Core.Data.Filters;
using Candidatos.Business.Enums;

namespace Candidatos.Business.Filters;

public class CandidatoFilter : Filter
{
    public Guid? Id { get; set; }
    public int? Codigo { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? CelularAlternativo { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public Sexo? Sexo { get; set; }
    public StatusCandidato? Status { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
